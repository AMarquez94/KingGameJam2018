using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase1EnemyController : MonoBehaviour {

    public GameObject bullet;

    public Transform playerPos;

    private float timerForAttack;

    private enemyController enemyCont;

    private CharacterBulletPower bullet_power;

    private void Start()
    {
        bullet_power = GetComponent<CharacterBulletPower>();
        enemyCont = GetComponent<enemyController>();
        if (enemyCont == null)
        {
            enemyCont = this.gameObject.AddComponent<enemyController>();
            Debug.LogWarning("Te has dejau de poner el enemyController crack");
        }

    }

    void Update()
    {
        timerForAttack += Time.deltaTime;
        if (timerForAttack >= enemyCont.attackFrecuency)
        {
            Vector3 dirToPlayer = playerPos.position - this.transform.position;
            GameObject bullet_go = Instantiate(bullet, this.transform.position + this.transform.forward * 0.1f, Quaternion.identity);
            bullet_go.transform.forward = dirToPlayer;
            BulletController bull_cont = bullet_go.GetComponent<BulletController>();
            bull_cont.setVariables(bullet_power.bullet_damage, bullet_power.bullet_speed, bullet_power.bullet_range);
            timerForAttack = 0.0f;
        }
    }
}
