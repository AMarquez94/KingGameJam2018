using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LevelGenerator : MonoBehaviour {

    public static LevelGenerator instance;

    // Deprecated
    public enum DoorSide
    {
        DOOR_LEFT,
        DOOR_RIGHT,
        DOOR_TOP,
        DOOR_BOTTOM
    }

    public Vector2 grid_size;
    public Vector2 tile_size;
    public Vector3 camera_offset;

    public GameObject tile_prefab;

    private Vector2 actual_tile;
    public List<List<GameObject>> grid_objects;

    private GameObject _start_tile;
    private GameObject _boss_tile;
    private GameObject _current_tile;

    private List<Vector2> _root_path;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {

        grid_objects = new List<List<GameObject>>();
        GenerateTiles();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateTiles()
    {
        // Size of 15 per 10
        for(int i = 0; i < grid_size.x; i++)
        {
            grid_objects.Add(new List<GameObject>());
            for (int j = 0; j < grid_size.y; j++)
            {
                GameObject instance = Instantiate(tile_prefab, new Vector3(i * tile_size.x, 0, j * tile_size.y), Quaternion.EulerAngles(0,180 * Mathf.Deg2Rad,0)) as GameObject;
                instance.transform.SetParent(transform);
                instance.name = "Tile_" + i + "-" + j;
                instance.GetComponent<RoomController>().grid_pos = new Vector2(i, j);

                grid_objects.Last().Add(instance);
            }
        }

        SeedScene();
        SeedStart();
        GeneratePath();
        DisableDoors();
        InitRooms();
    }

    // Method to determine if door is present on given side
    public void SeedScene()
    {
        for (int i = 0; i < grid_objects.Count; i++)
        {
            for (int j = 0; j < grid_objects[i].Count; j++)
            {
                if(i == 0) grid_objects[i][j].transform.Find("Tile_LDoor").gameObject.SetActive(false);
                if(j == 0) grid_objects[i][j].transform.Find("Tile_BDoor").gameObject.SetActive(false);
                if (i == (grid_size.x - 1)) grid_objects[i][j].transform.Find("Tile_RDoor").gameObject.SetActive(false);
                if (j == (grid_size.y - 1)) grid_objects[i][j].transform.Find("Tile_TDoor").gameObject.SetActive(false);

                grid_objects[i][j].GetComponent<RoomController>().grid_pos = new Vector2(i, j);
            }
        }
    }

    public void SeedStart()
    {
        // Finally pick one of the rooms as our spawn.
        actual_tile.x = (int)UnityEngine.Random.Range(1, grid_size.x - 3);
        actual_tile.y = (int)UnityEngine.Random.Range(1, grid_size.y - 2);

        {
            _current_tile = grid_objects[(int)actual_tile.x][(int)actual_tile.y];
            _current_tile.transform.Find("Tile_LDoor").gameObject.SetActive(false);
            _current_tile.transform.Find("Tile_BDoor").gameObject.SetActive(false);
            _current_tile.transform.Find("Tile_TDoor").gameObject.SetActive(false);
            _current_tile.SetActive(true);
            _start_tile = _current_tile;
        }

        {
            _boss_tile = grid_objects[(int)actual_tile.x + 1][(int)actual_tile.y + 1];
            _boss_tile.transform.Find("Tile_LDoor").gameObject.SetActive(false);
            _boss_tile.transform.Find("Tile_RDoor").gameObject.SetActive(false);
            _boss_tile.transform.Find("Tile_TDoor").gameObject.SetActive(false);
        }

        // Debug purposes
        {
            GameObject start_point = GameObject.CreatePrimitive(PrimitiveType.Cube);
            start_point.transform.position = _start_tile.transform.position + new Vector3(0, 1, 0);
            start_point.GetComponent<MeshRenderer>().material.color = Color.green;

            GameObject boss_point = GameObject.CreatePrimitive(PrimitiveType.Cube);
            boss_point.transform.position = _boss_tile.transform.position + new Vector3(0, 1, 0);
            boss_point.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = _current_tile.transform.position;
        Camera.main.transform.position = _current_tile.transform.position + camera_offset;
        Debug.Log(player.transform.position);
    }

    public void DisableDoors()
    {
        for (int i = 0; i < grid_objects.Count; i++)
        {
            for (int j = 0; j < grid_objects[i].Count; j++)
            {
                if (!_root_path.Contains(new Vector2(i, j)))
                    grid_objects[i][j].SetActive(false);
            }
        }

        Vector2 n_pos = _start_tile.GetComponent<RoomController>().grid_pos + new Vector2(1, 0);

        for (int i = 0; i < grid_objects.Count; i++)
        {
            for (int j = 0; j < grid_objects[i].Count; j++)
            {
                if (i == n_pos.x && j == n_pos.y)
                    continue;

                if (i > 0 && !grid_objects[i - 1][j].activeSelf)
                    grid_objects[i][j].transform.Find("Tile_LDoor").gameObject.SetActive(false);

                if (j > 0 && !grid_objects[i][j - 1].activeSelf)
                    grid_objects[i][j].transform.Find("Tile_BDoor").gameObject.SetActive(false);

                if ((i < grid_objects.Count - 1) && !grid_objects[i + 1][j].activeSelf)
                    grid_objects[i][j].transform.Find("Tile_RDoor").gameObject.SetActive(false);

                if ((j < grid_objects[i].Count - 1) && !grid_objects[i][j + 1].activeSelf)
                    grid_objects[i][j].transform.Find("Tile_TDoor").gameObject.SetActive(false);


                if (i > 0 && (grid_objects[i - 1][j] == _start_tile || grid_objects[i - 1][j] == _boss_tile))
                    grid_objects[i][j].transform.Find("Tile_LDoor").gameObject.SetActive(false);

                if (j > 0 && (grid_objects[i][j - 1] == _start_tile || grid_objects[i][j - 1] == _boss_tile))
                    grid_objects[i][j].transform.Find("Tile_BDoor").gameObject.SetActive(false);

                if ((i < grid_objects.Count - 1) && (grid_objects[i + 1][j] == _start_tile || grid_objects[i + 1][j] == _boss_tile))
                    grid_objects[i][j].transform.Find("Tile_RDoor").gameObject.SetActive(false);

                if ((j < grid_objects[i].Count - 1) && (grid_objects[i][j + 1] == _start_tile || grid_objects[i][j + 1] == _boss_tile))
                    grid_objects[i][j].transform.Find("Tile_TDoor").gameObject.SetActive(false);
            }
        }
    }

    public void InitRooms()
    {
        for (int i = 0; i < grid_objects.Count; i++)
        {
            for (int j = 0; j < grid_objects[i].Count; j++)
            {
                grid_objects[i][j].GetComponent<RoomController>().InitRoom();

                if (grid_objects[i][j] != _start_tile)
                    grid_objects[i][j].SetActive(false);
            }
        }
    }

    // Method to change the actual room we are in.
    public void ChangeTile(string door_tag)
    {
        _current_tile.SetActive(false);
        _current_tile.GetComponent<RoomController>().DestroyRoom();

        GameObject door = _current_tile.transform.Find("Tile_BDoor").gameObject;

        switch (door_tag)
        {
            case "TDoor":{ actual_tile.y++; } break;
            case "BDoor":{ actual_tile.y--; } break;
            case "LDoor":{ actual_tile.x--; } break;
            case "RDoor":{ actual_tile.x++; } break;
        }

        _current_tile = grid_objects[(int)actual_tile.x][(int)actual_tile.y];
        _current_tile.GetComponent<RoomController>().InitRoom();
        _current_tile.SetActive(true);

        switch (door_tag)
        {
            case "TDoor": { door = _current_tile.transform.Find("Tile_BDoor").gameObject; } break;
            case "BDoor": { door = _current_tile.transform.Find("Tile_TDoor").gameObject; } break;
            case "LDoor": { door = _current_tile.transform.Find("Tile_RDoor").gameObject; } break;
            case "RDoor": { door = _current_tile.transform.Find("Tile_LDoor").gameObject; } break;
        }

        // Teleport the player here to it's new tile.
        // Apply camera transition

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(door.transform.position.x, transform.position.y - 2.5f, door.transform.position.z);
        player.transform.position += (_current_tile.transform.position - door.transform.position).normalized;
        Camera.main.transform.position = _current_tile.transform.position + camera_offset;
    }

    public void GeneratePath()
    {
        Vector2 origpos = _start_tile.GetComponent<RoomController>().grid_pos;
        Vector2 launch_point = origpos + new Vector2(1, -1);
        Vector2 boss_point = origpos + new Vector2(1, 1);
        Vector2 desired_point = origpos + new Vector2(2, 0);

        Vector2 current_it = launch_point;

        // Adding rooms which must be in the path
        _root_path = new List<Vector2>();
        _root_path.Add(origpos);
        _root_path.Add(origpos + new Vector2(1,1));
        _root_path.Add(origpos + new Vector2(1, 0));
        //_root_path.Add(desired_point);
        _root_path.Add(launch_point);

        int i = 0;
        int max_iterations = (int)(grid_size.x * grid_size.y);
        while (i < max_iterations)
        {
            List<Vector2> posible_points = new List<Vector2>();
            Vector2 desired_top = current_it + new Vector2(0, 1);
            Vector2 desired_bot = current_it + new Vector2(0, -1);
            Vector2 desired_left = current_it + new Vector2(-1, 0);
            Vector2 desired_right = current_it + new Vector2(1, 0);
            desired_top = new Vector2((int)desired_top.x, (int)desired_top.y);
            desired_bot = new Vector2((int)desired_bot.x, (int)desired_bot.y);
            desired_left = new Vector2((int)desired_left.x, (int)desired_left.y);
            desired_right = new Vector2((int)desired_right.x, (int)desired_right.y);

            if (!_root_path.Contains(desired_top) && current_it.y < (grid_size.y - 1)) posible_points.Add(desired_top);
            if (!_root_path.Contains(desired_bot) && current_it.y > 0) posible_points.Add(desired_bot);
            if (!_root_path.Contains(desired_left) && current_it.x > 0) posible_points.Add(desired_left);
            if (!_root_path.Contains(desired_right) && current_it.x < (grid_size.x - 1)) posible_points.Add(desired_right);

            if (posible_points.Count < 1)
            {
                // We Failed to generate
                int r_cell = (int)UnityEngine.Random.Range(5, _root_path.Count - 1);
                current_it = _root_path[r_cell];
                //_root_path.RemoveAt(r_cell);
                i++;
                continue;
            }

            // Determine one of the posible doors
            int next_point = (int)UnityEngine.Random.Range(0, posible_points.Count - 1);
            current_it = posible_points[next_point];
            _root_path.Add(current_it);

            // We found the final path
            if (current_it == desired_point)
                break;

            i++;
        }

        if (!_root_path.Contains(desired_point))
        {
            GeneratePath();
            return;
        }

        // Erase some unnecessary nodes so it is more random
        if(_root_path.Count > 10)
        {
            List<Vector2> to_remove = new List<Vector2>();
            for (int x = 5; x < _root_path.Count - 4; x++)
            {
                Vector2 desired_top = _root_path[x] + new Vector2(0, 1);
                Vector2 desired_bot = _root_path[x] + new Vector2(0, -1);
                Vector2 desired_left = _root_path[x] + new Vector2(-1, 0);
                Vector2 desired_right = _root_path[x] + new Vector2(1, 0);

                Vector2 desired_ltop = _root_path[x] + new Vector2(-1, 1);
                Vector2 desired_lbot = _root_path[x] + new Vector2(-1, -1);
                Vector2 desired_rbot = _root_path[x] + new Vector2(1, -1);
                Vector2 desired_rtop = _root_path[x] + new Vector2(1, 1);

                if (to_remove.Contains(desired_top) || to_remove.Contains(desired_bot)
                    || to_remove.Contains(desired_left) || to_remove.Contains(desired_right)
                    || to_remove.Contains(desired_ltop) || to_remove.Contains(desired_lbot)
                    || to_remove.Contains(desired_rbot) || to_remove.Contains(desired_rtop))
                    continue;

                if (_root_path.Contains(desired_top) && _root_path.Contains(desired_bot)
                    && _root_path.Contains(desired_left) && _root_path.Contains(desired_right)
                    && _root_path.Contains(desired_ltop) && _root_path.Contains(desired_lbot)
                    && _root_path.Contains(desired_rbot) && _root_path.Contains(desired_rtop))
                    to_remove.Add(_root_path[x]);
            }

            _root_path = _root_path.Except(to_remove).ToList();
        }

        for (int x = 0; x < _root_path.Count; x++)
        {
            GameObject start_point = GameObject.CreatePrimitive(PrimitiveType.Cube);
            start_point.transform.localPosition = grid_objects[(int)_root_path[x].x][(int)_root_path[x].y].transform.localPosition + new Vector3(0, 5, 0);
            start_point.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
}
