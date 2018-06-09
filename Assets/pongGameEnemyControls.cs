using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pongGameEnemyControls : MonoBehaviour {

    public Transform bola;
    public pongGamePlayerController controller;

    void Update () {

        controller.upArrowPressed = false;
        controller.downArrowPressed = false;
        if (bola.position.y < this.transform.position.y - 0.05f)
        {
            controller.downArrowPressed = true;
        }
        if (bola.position.y > this.transform.position.y + 0.05f)
        {
            controller.upArrowPressed = true;
        }

    }

}
