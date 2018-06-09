using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pongGameBallController : MonoBehaviour {

    public float speed = 2.0f;
    Vector3 direction;

	// Use this for initialization
	void Start () {
        //direction = Vector3.up + Vector3.right;
        direction = -Vector3.right;
        direction.Normalize();
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += direction * Time.deltaTime * speed;

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "pongRaqueta")
        {
            float diff = transform.position.y - other.gameObject.transform.position.y;
            diff = Mathf.Clamp(diff,-1,1);
            diff = (diff + 1) / 2;
            Vector3 upV = (Vector3.up * 2 + Vector3.right).normalized;
            Vector3 downV = (Vector3.down * 2 + Vector3.right).normalized;
            Vector3 newdir = Vector3.Lerp(downV, upV, diff).normalized;
            if (direction.x < 0) newdir.x = Mathf.Abs(newdir.x);
            else newdir.x = -Mathf.Abs(newdir.x);

            direction = newdir;
        }

        if (other.gameObject.tag == "pongPared")
        {
            direction.y *= -1;
        }

        direction.Normalize();
    }
}
