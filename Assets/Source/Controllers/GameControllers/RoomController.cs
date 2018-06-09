using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {

    public bool room_clear;
    public Vector2 grid_pos;

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

    }
}
