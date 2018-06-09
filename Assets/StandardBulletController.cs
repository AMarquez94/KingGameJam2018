using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBulletController : MonoBehaviour {

    private BulletController bulletCont;
	
	void Start () {
        bulletCont = GetComponent<BulletController>();
        if(bulletCont == null)
        {
            bulletCont = this.gameObject.AddComponent<BulletController>();
        }
    }
	
	void Update () {

        this.transform.position += this.transform.forward * Time.deltaTime * bulletCont.speed;

    }
}
