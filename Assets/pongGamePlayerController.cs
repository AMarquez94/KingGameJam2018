using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pongGamePlayerController : MonoBehaviour {

    public bool upArrowPressed = false;
    public bool downArrowPressed = false;
    public float speed = 2.0f;
	void Update () {

        int dir = 0;
		if(upArrowPressed)
        {
            dir = 1;
        }
        if (downArrowPressed)
        {
            dir = -1;
        }
        if (upArrowPressed && downArrowPressed)
        {
            dir = 0;
        }
        Vector3 deltaMovement = new Vector3( 0, Time.deltaTime * speed * dir, 0 );
        Vector3 newPos = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + deltaMovement.y, -2.64f, 4.3f), transform.position.z);
        transform.position = newPos;

    }
}
