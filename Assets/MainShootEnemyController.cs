using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShootEnemyController : MonoBehaviour {

    public GameObject bullet;

    public Transform playerPos;

    private float timerForAttack;

    private enemyController enemyCont;

    private float rangeTime = 0.0f;

    private float totalTime = 0.0f;

    private int signRand = 0;



    private void Start()
    {
        enemyCont = GetComponent<enemyController>();
        if(enemyCont == null)
        {
            enemyCont = this.gameObject.AddComponent<enemyController>();
            Debug.LogWarning("Te has dejau de poner el enemyController crack");
        }
        playerPos = GameControllerManager.getGameControllerManager().getPlayer().transform;
        if (Random.Range(0f, 1.0f) >= 0.5)
        {
            signRand = 1;
        }
        else
        {
            signRand = -1;
        }
        totalTime = Random.Range(0f,10f);

    }

    void Update () {

        totalTime += Time.deltaTime * signRand;
        this.transform.position += new Vector3(Mathf.Sin(totalTime), 0, Mathf.Cos(totalTime)) * Time.deltaTime * 0.25f;
        this.transform.LookAt(playerPos.position);
        timerForAttack += Time.deltaTime;
        if(timerForAttack >= enemyCont.attackFrecuency + rangeTime)
        {
            Vector3 dirToPlayer = playerPos.position - this.transform.position;
            GameObject bullet_go = Instantiate(bullet, this.transform.position + this.transform.forward * 0.1f,Quaternion.identity);
            bullet_go.transform.forward = dirToPlayer;
            bullet_go.GetComponent<BulletController>().setVariables(enemyCont.character_bullet_controller.bullet_damage, enemyCont.character_bullet_controller.bullet_speed, enemyCont.character_bullet_controller.bullet_range);
            bullet_go.GetComponent<BulletController>().SetSender(this.gameObject);
            timerForAttack = 0.0f;
            rangeTime = Random.Range(-1.0f,1.0f);
        }
	}

    public void changeTexture(Texture text)
    {
        
    }

}
