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

	// Use this for initialization
	void Start () {

        room_clear = false; 
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
        // Close all the blocked doors, we can pick different objects depending on type
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).transform.gameObject;

            if (child.tag != "Scenario" && !child.activeSelf)
                BuildBlock(child.transform.position);
        }

        // Place the obstacles
        int index = 0;
        for(int i = 0; i < max_obstacles; i++)
        {

        }
    }

    public void BuildBlock(Vector3 pos)
    {
        // Load a prefab instead.
        GameObject new_block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        new_block.transform.position = pos;
        new_block.transform.localScale = new Vector3(2, 2, 2);
    }

    public void BuildObstacle(Vector3 pos)
    {
        // Load a prefab instead.
        GameObject new_block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        new_block.transform.position = pos;
        new_block.transform.localScale = new Vector3(1, 1, 1);
    }
}
