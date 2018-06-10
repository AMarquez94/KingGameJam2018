using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBulletController : MonoBehaviour {

    public GameObject bullet;
    private BulletController bull_cont;
    public float factorReduction = 0.5f;
    void Start()
    {
        bull_cont = GetComponent<BulletController>();
        GameObject bullet1 = Instantiate(bullet, this.transform.position, Quaternion.identity);
        GameObject bullet2 = Instantiate(bullet, this.transform.position, Quaternion.identity);
        GameObject bullet3 = Instantiate(bullet, this.transform.position, Quaternion.identity);
        GameObject bullet4 = Instantiate(bullet, this.transform.position, Quaternion.identity);
        GameObject bullet5 = Instantiate(bullet, this.transform.position, Quaternion.identity);
        GameObject bullet6 = Instantiate(bullet, this.transform.position, Quaternion.identity);
        GameObject bullet7 = Instantiate(bullet, this.transform.position, Quaternion.identity);
        GameObject bullet8 = Instantiate(bullet, this.transform.position, Quaternion.identity);
        bullet1.GetComponent<BulletController>().setVariables(bull_cont.damage, bull_cont.speed, bull_cont.range);
        bullet1.GetComponent<BulletController>().SetSender(bull_cont.GetSender());
        bullet1.transform.forward = Vector3.forward;
        bullet1.transform.localScale *= factorReduction;
        bullet2.GetComponent<BulletController>().setVariables(bull_cont.damage, bull_cont.speed, bull_cont.range); ;
        bullet2.GetComponent<BulletController>().SetSender(bull_cont.GetSender());
        bullet2.transform.forward = (Vector3.forward + Vector3.right).normalized;
        bullet2.transform.localScale *= factorReduction;
        bullet3.GetComponent<BulletController>().setVariables(bull_cont.damage, bull_cont.speed, bull_cont.range); ;
        bullet3.GetComponent<BulletController>().SetSender(bull_cont.GetSender());
        bullet3.transform.forward = Vector3.right;
        bullet3.transform.localScale *= factorReduction;
        bullet4.GetComponent<BulletController>().setVariables(bull_cont.damage, bull_cont.speed, bull_cont.range); ;
        bullet4.GetComponent<BulletController>().SetSender(bull_cont.GetSender());
        bullet4.transform.forward = (Vector3.right + Vector3.back).normalized;
        bullet4.transform.localScale *= factorReduction;
        bullet5.GetComponent<BulletController>().setVariables(bull_cont.damage, bull_cont.speed, bull_cont.range); ;
        bullet5.GetComponent<BulletController>().SetSender(bull_cont.GetSender());
        bullet5.transform.forward = Vector3.back;
        bullet5.transform.localScale *= factorReduction;
        bullet6.GetComponent<BulletController>().setVariables(bull_cont.damage, bull_cont.speed, bull_cont.range); ;
        bullet6.GetComponent<BulletController>().SetSender(bull_cont.GetSender());
        bullet6.transform.forward = (Vector3.back + Vector3.left).normalized;
        bullet6.transform.localScale *= factorReduction;
        bullet7.GetComponent<BulletController>().setVariables(bull_cont.damage, bull_cont.speed, bull_cont.range); ;
        bullet7.GetComponent<BulletController>().SetSender(bull_cont.GetSender());
        bullet7.transform.forward = Vector3.left;
        bullet7.transform.localScale *= factorReduction;
        bullet8.GetComponent<BulletController>().setVariables(bull_cont.damage, bull_cont.speed, bull_cont.range); ;
        bullet8.GetComponent<BulletController>().SetSender(bull_cont.GetSender());
        bullet8.transform.forward = (Vector3.left + Vector3.forward).normalized;
        bullet8.transform.localScale *= factorReduction;
        Destroy(this.gameObject);
    }
}
