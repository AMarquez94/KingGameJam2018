using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerManager : MonoBehaviour {

    static GameControllerManager _instance;
    public GameObject player;

    public bool fading;
    const float INCREMENT = 0.01f;
    const float MAX_BLEND = 2;

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

    void Update()
    {
        //if (fading)
        //{
        //    fadeValue += INCREMENT;
        //    so.intensity = fadeValue * MAX_BLEND;

        //    if (fadeValue >= 1)
        //    {
        //        fading = false;
        //        // change player position
        //    }
        //}
        //else if (fadeValue > 0)
        //{
        //    fadeValue = Mathf.Max(0, fadeValue - INCREMENT);
        //    so.intensity = fadeValue * MAX_BLEND;
        //}
    }
}
