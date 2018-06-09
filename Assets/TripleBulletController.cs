using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBulletController : MonoBehaviour {

    public GameObject bullet;
    private BulletController bull_cont;

	void Start () {
        bull_cont = GetComponent<BulletController>();
        GameObject bullet1 = Instantiate(bullet, this.transform.position, this.transform.rotation);
        GameObject bullet2 = Instantiate(bullet, this.transform.position, this.transform.rotation * Quaternion.Euler(0,20,0));
        GameObject bullet3 = Instantiate(bullet, this.transform.position, this.transform.rotation * Quaternion.Euler(0, -20, 0));
        bullet1.GetComponent<BulletController>().setVariables(bull_cont.damage, bull_cont.speed, bull_cont.range);
        bullet2.GetComponent<BulletController>().setVariables(bull_cont.damage, bull_cont.speed, bull_cont.range); ;
        bullet3.GetComponent<BulletController>().setVariables(bull_cont.damage, bull_cont.speed, bull_cont.range); ;
    }
}
