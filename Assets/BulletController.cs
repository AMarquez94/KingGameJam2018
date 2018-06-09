using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float damage = 10.0f;
    public float speed = 3.0f;
    public float range = 10.0f;

    public float minDamage;
    public float maxDamage;
    public float minSpeed;
    public float maxSpeed;
    public float minRange;
    public float maxRange;

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

    public void SetMutation(MutationController.BulletMutation bulletMutation)
    {
        damage = Mathf.Clamp(damage + bulletMutation.bulletDamageModifier, minDamage, maxDamage);
        speed = Mathf.Clamp(speed + bulletMutation.bulletSpeedModificer, minSpeed, maxSpeed);
        range = Mathf.Clamp(range + bulletMutation.bulletRangeModifier, minRange, maxRange);
    }

}
