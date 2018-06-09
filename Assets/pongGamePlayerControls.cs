using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pongGamePlayerControls : MonoBehaviour {

    public pongGamePlayerController controller;
	
	void Update () {

        controller.upArrowPressed = false;
        controller.downArrowPressed = false;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            controller.upArrowPressed = true;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            controller.downArrowPressed = true;
        }
    }
}
