using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pongGameLimitController : MonoBehaviour {


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "pongBall")
        {
            print("C'est fini");
        }

    }

}
