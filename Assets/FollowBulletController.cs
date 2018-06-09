using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBulletController : MonoBehaviour {

    private BulletController bulletCont;
    public float angularSpeed = 1.0f;
    private Transform playerTransform;

    void Start()
    {
        bulletCont = GetComponent<BulletController>();
        if (bulletCont == null)
        {
            bulletCont = this.gameObject.AddComponent<BulletController>();
        }
        playerTransform = GameControllerManager.getGameControllerManager().getPlayer().transform;
    }

    void Update()
    {
        Vector3 dirToPlayer = playerTransform.position - this.transform.position;
        float angle = Vector3.Angle(this.transform.forward,dirToPlayer);
        Vector3 cross = Vector3.Cross(this.transform.forward, dirToPlayer);
        if (cross.y < 0) angle = -angle;
        this.transform.position += this.transform.forward * Time.deltaTime * bulletCont.speed;
        this.transform.Rotate(0,angle * Time.deltaTime * angularSpeed, 0);

    }
}
