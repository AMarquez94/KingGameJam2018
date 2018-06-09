using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    // This door room manager
    private RoomController room_manager;
    
	// Use this for initialization
	void Start () {

        room_manager = transform.parent.GetComponent<RoomController>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        // Do the swap logic here
        if (room_manager.room_clear && other.tag == "Player")
            LevelGenerator.instance.ChangeTile(this.tag);

        // Destroy everything on the actual room;
    }
}
