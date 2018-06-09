using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LevelGenerator : MonoBehaviour {

    public enum DoorSide
    {
        DOOR_LEFT,
        DOOR_RIGHT,
        DOOR_TOP,
        DOOR_BOTTOM
    }

    public Vector2 grid_size;
    public Vector2 tile_size;

    public GameObject tile_prefab;
    public List<List<GameObject>> grid_objects;

    private Vector2 actual_tile;
    private GameObject _current_tile;

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
                GameObject instance = Instantiate(tile_prefab, new Vector3(i * tile_size.x, 0, j * tile_size.y), Quaternion.identity) as GameObject;
                instance.transform.SetParent(transform);
                instance.SetActive(false);

                Debug.Log("Spawned a tile");
                grid_objects.Last().Add(instance);
            }
        }

        // Finally pick one of the rooms as our spawn.
        int h_pos = (int)UnityEngine.Random.Range(0, grid_size.x);
        int v_pos = (int)UnityEngine.Random.Range(0, grid_size.y);

        _current_tile = grid_objects[h_pos][v_pos];
        _current_tile.SetActive(true);
        actual_tile = new Vector2(h_pos, v_pos);
    }

    // Method to determine if door is present on given side
    public void SeedScene()
    {
        for(int i = 0; i < grid_objects.Count; i++)
        {
            for (int j = 0; j < grid_objects[i].Count; j++)
            {
                //if(j = 0) grid_objects[i][j].FindWithTag("TDoor").gameObject.SetActive(false);
            }
        }
    }

    public void ChangeTile(DoorSide door_dir)
    {
        _current_tile.SetActive(false);

        switch (door_dir)
        {

        }


        // Teleport the player here to it's new tile.
    }
}
