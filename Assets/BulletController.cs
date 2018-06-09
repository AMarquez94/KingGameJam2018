using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float damage = 10.0f;
    public float speed = 3.0f;
    public float range = 10.0f;

    private float ammountMoved = 0.0f;
    private Vector3 lastFramePosition;

    private void Start()
    {
        lastFramePosition = this.transform.position;
    }

    private void Update()
    {
        ammountMoved += (this.transform.position - lastFramePosition).magnitude;
        if(ammountMoved >= range)
        {
            destroyBullet();
        }
        lastFramePosition = this.transform.position; 
    }

    private void destroyBullet()
    {
        Destroy(this.gameObject);
    }

}
