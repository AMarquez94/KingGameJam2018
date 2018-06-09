using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerManager : MonoBehaviour {

    static GameControllerManager _instance;
    public GameObject player;

	void Awake () {
        _instance = this;
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
	}
	
    public static GameControllerManager getGameControllerManager()
    {
        if(_instance == null)
        {
            GameObject new_inst = new GameObject("GameControllerManager");
            _instance = new_inst.AddComponent<GameControllerManager>();
        }
        return _instance;
    }

    public GameObject getPlayer()
    {
        return _instance.player;
    }
}
