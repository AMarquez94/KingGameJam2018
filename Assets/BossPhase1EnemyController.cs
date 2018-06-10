using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase1EnemyController : MonoBehaviour {

    public enum BossPhase1Attack
    {
        Area,
        Spawn,
        Teleport,
        TripleAttack,
        Mirror,
        Dash
    };

    public GameObject bullet;

    public GameObject area_bullet;

    public GameObject triple_bullet;

    public GameObject follow_bullet;

    public Transform playerPos;

    private float timerForAttack;

    private enemyController enemyCont;

    private CharacterBulletPower bullet_power;

    public GameObject[] enemiesToSpawn;

    private BossPhase1Attack nextAttack = BossPhase1Attack.Teleport;
    private bool doingAttack = false;

    private float timeToTeleport = 1.0f;
    private float teleportTimer = 0.0f;
    public GameObject teleportParticles;
    private Vector3 teleportPoint;
    private BossPhase1Attack[] mappingAttacks = { BossPhase1Attack.Area, BossPhase1Attack.Spawn, BossPhase1Attack.Teleport, BossPhase1Attack.TripleAttack};

    private int signRand = 0;
    private float totalTime = 0;
    private void Start()
    {
        
        bullet_power = GetComponent<CharacterBulletPower>();
        enemyCont = GetComponent<enemyController>();
        if (enemyCont == null)
        {
            enemyCont = this.gameObject.AddComponent<enemyController>();
            Debug.LogWarning("Te has dejau de poner el enemyController crack");
        }
        playerPos = GameControllerManager.getGameControllerManager().getPlayer().transform;
        teleportParticles.SetActive(false);
        if (Random.Range(0f, 1.0f) >= 0.5)
        {
            signRand = 1;
        }
        else
        {
            signRand = -1;
        }
        totalTime = Random.Range(0f, 10f);
    }

    void Update()
    {
        timerForAttack += Time.deltaTime;
        totalTime += Time.deltaTime * signRand;
        this.transform.position += new Vector3(Mathf.Sin(totalTime), 0, Mathf.Cos(totalTime)) * Time.deltaTime * 0.25f;
        this.transform.LookAt(playerPos.position);
        if (timerForAttack >= enemyCont.attackFrecuency && !doingAttack)
        {
            switch (nextAttack)
            {
                case BossPhase1Attack.Area:

                    break;

                case BossPhase1Attack.Dash:

                    break;

                case BossPhase1Attack.Mirror:

                    break;

                case BossPhase1Attack.Spawn:

                    break;

                case BossPhase1Attack.Teleport:

                    Vector3 tile_pos = LevelGenerator.instance.GetBossTile().transform.position;
                    Vector2 tile_size = LevelGenerator.instance.tile_size;
                    teleportPoint = new Vector3(Random.Range(tile_pos.x - (tile_size.x / 4), tile_pos.x + (tile_size.x / 4)), 1.0f, Random.Range(tile_pos.z - (tile_size.y / 4), tile_pos.z + (tile_size.y / 4)));
                    teleportParticles.transform.position = teleportPoint;
                    teleportParticles.SetActive(true);
                    break;

                case BossPhase1Attack.TripleAttack:

                    break;

            }
            doingAttack = true;        
        }

        if (doingAttack)
        {
            Vector3 dirToPlayer;
            GameObject bullet_go;
            BulletController bull_cont;
            switch (nextAttack)
            {

                case BossPhase1Attack.Area:
                    dirToPlayer = playerPos.position - this.transform.position;
                    bullet_go = Instantiate(area_bullet, this.transform.position + this.transform.forward * 0.1f, Quaternion.identity);
                    bullet_go.transform.forward = dirToPlayer;
                    bull_cont = bullet_go.GetComponent<BulletController>();
                    bull_cont.setVariables(bullet_power.bullet_damage, bullet_power.bullet_speed, bullet_power.bullet_range);
                    bull_cont.SetSender(this.gameObject);
                    timerForAttack = 0.0f;
                    doingAttack = false;
                    RandomNextAttack();
                    break;

                case BossPhase1Attack.Dash:

                    break;

                case BossPhase1Attack.Mirror:

                    break;

                case BossPhase1Attack.Spawn:
                    int randEnemyIndex = Random.Range(0,enemiesToSpawn.Length);
                    GameObject enemy = Instantiate(enemiesToSpawn[randEnemyIndex], transform.position + Random.Range(-3.0f, 3.0f) * Vector3.right + Random.Range(-3.0f, 3.0f) * Vector3.forward, Quaternion.identity);
                    enemy.GetComponent<CharacterBulletPower>().bullet_damage = 10;
                    enemy.GetComponent<CharacterBulletPower>().bullet_speed = 10;
                    enemy.GetComponent<CharacterBulletPower>().bullet_range = 20;
                    enemy.GetComponent<enemyController>().attackFrecuency = 2;
                    timerForAttack = 0.0f;
                    doingAttack = false;
                    RandomNextAttack();
                    break;

                case BossPhase1Attack.Teleport:
                    teleportTimer += Time.deltaTime;
                    if(teleportTimer > timeToTeleport)
                    {                    
                        this.transform.position = teleportPoint;
                        timerForAttack = 0.0f;
                        doingAttack = false;
                        teleportTimer = 0.0f;
                        teleportParticles.SetActive(false);
                        RandomNextAttack();
                    }
                    
                    break;

                case BossPhase1Attack.TripleAttack:
                    dirToPlayer = playerPos.position - this.transform.position;
                    bullet_go = Instantiate(triple_bullet, this.transform.position + this.transform.forward * 0.1f, Quaternion.identity);
                    bullet_go.transform.forward = dirToPlayer;
                    bull_cont = bullet_go.GetComponent<BulletController>();
                    bull_cont.SetSender(this.gameObject);
                    bull_cont.setVariables(bullet_power.bullet_damage, bullet_power.bullet_speed, bullet_power.bullet_range);
                    timerForAttack = 0.0f;

                    doingAttack = false;
                    RandomNextAttack();
                    break;

            }
        }
    }

    private void RandomNextAttack()
    {
        float random_num = Random.Range(0.0f, 1.0f);
        if(random_num < 0.35f)
        {
            nextAttack = BossPhase1Attack.TripleAttack;
        }
        else if (random_num < 0.55f)
        {
            nextAttack = BossPhase1Attack.Spawn;
        }
        else if (random_num < 0.85f)
        {
            nextAttack = BossPhase1Attack.Area;
        }
        else
        {
            nextAttack = BossPhase1Attack.Teleport;
        }


    }

}
