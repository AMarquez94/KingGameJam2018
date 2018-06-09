﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShootEnemyController : MonoBehaviour {

    public GameObject bullet;

    public Transform playerPos;

    private float timerForAttack;

    private enemyController enemyCont;

    private void Start()
    {
        enemyCont = GetComponent<enemyController>();
        if(enemyCont == null)
        {
            enemyCont = this.gameObject.AddComponent<enemyController>();
            Debug.LogWarning("Te has dejau de poner el enemyController crack");
        }

    }

    void Update () {
        timerForAttack += Time.deltaTime;
        if(timerForAttack >= enemyCont.attackFrecuency)
        {
            Vector3 dirToPlayer = playerPos.position - this.transform.position;
            GameObject bullet_go = Instantiate(bullet, this.transform.position + this.transform.forward * 0.1f,Quaternion.identity);
            bullet_go.transform.forward = dirToPlayer;
            timerForAttack = 0.0f;
        }
	}

}