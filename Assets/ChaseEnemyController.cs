using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseEnemyController : MonoBehaviour {

    public Transform playerPos;
    public enemyController enemyCont;
    public NavMeshAgent agent;

    void Start () {
        enemyCont = GetComponent<enemyController>();
        if(enemyCont == null)
        {
            this.gameObject.AddComponent<enemyController>();
        }
        
    }
	
	void Update () {
        agent.destination = playerPos.position;

    }
}
