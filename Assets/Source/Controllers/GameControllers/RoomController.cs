using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {

    public bool room_clear;
    public Vector2 grid_pos;
    public int max_obstacles;

    private RoomController left;
    private RoomController right;
    private RoomController top;
    private RoomController bottom;

    void Start()
    {

        room_clear = false;
    }
	// Use this for initialization
	public void BuildRoom () {

        GameObject root_obstacles = transform.GetChild(0).gameObject;

        // Close all the blocked doors, we can pick different objects depending on type
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).transform.gameObject;

            if (child.tag != "Scenario" && !child.activeSelf)
            {
                GameObject obstacle = BuildBlock(child.transform.position + new Vector3(0, -1f, 0));
                obstacle.transform.SetParent(root_obstacles.transform);
            }
        }

        // Place the obstacles
        int obs_total = (int)UnityEngine.Random.Range(0, max_obstacles);
        float posx = transform.position.x - 5f;
        float posy = transform.position.z - 4f;

        int j = 0;
        List<Vector2> fetch = new List<Vector2>();
        while (j < obs_total)
        {
            int hpos = (int)UnityEngine.Random.Range(0, 10f);
            int vpos = (int)UnityEngine.Random.Range(0, 8f);
            Vector2 d_pos = new Vector2(hpos, vpos);
            if (fetch.Contains(d_pos))
                continue;

            GameObject obstacle = BuildObstacle(new Vector3(posx + hpos, 0, posy + vpos));// + new Vector3(0, -3f, 0));
            obstacle.transform.eulerAngles = new Vector3(-90, UnityEngine.Random.Range(0, 360), 0);
            obstacle.transform.SetParent(root_obstacles.transform);
            fetch.Add(d_pos);
            j++;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Destroy all the enemies and stuff inside this room here
    public void DestroyRoom()
    {

    }

    // Randomly spawn the entities in this room;
    public void InitRoom()
    {

    }

    public GameObject BuildBlock(Vector3 pos)
    {
        string prefab = (int)UnityEngine.Random.Range(0, 1) == 0 ? "Prefabs/Block1" : "Prefabs/Block2";

        // Load a prefab instead.
        GameObject new_block = Instantiate(Resources.Load(prefab, typeof(GameObject))) as GameObject;
        new_block.transform.position = pos;
        new_block.transform.localScale = new Vector3(2, 2, 2);

        return new_block;
    }

    public GameObject BuildObstacle(Vector3 pos)
    {
        // Load a prefab instead.
        //GameObject new_block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject new_block = Instantiate(Resources.Load("Prefabs/Obstacle", typeof(GameObject))) as GameObject;
        new_block.transform.position = pos;
        new_block.transform.localScale = new Vector3(1, 1, 1);

        return new_block;
    }
}
