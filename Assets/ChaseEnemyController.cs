using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseEnemyController : MonoBehaviour {

    public Transform playerPos;
    public enemyController enemyCont;

    void Start () {
        enemyCont = GetComponent<enemyController>();
        if(enemyCont == null)
        {
            this.gameObject.AddComponent<enemyController>();
        }
        playerPos = GameControllerManager.getGameControllerManager().getPlayer().transform;
    }
	
	void Update () {
        this.transform.position += (playerPos.position - this.transform.position).normalized * Time.deltaTime * enemyCont.movementSpeed;

    }
}
