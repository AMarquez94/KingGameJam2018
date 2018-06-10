using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {

    public float maxLife = 100;
    public float life = 100;
    public float movementSpeed = 4;
    public float attackFrecuency = 1.0f;

    public CharacterBulletPower character_bullet_controller;

    private void Start()
    {
        character_bullet_controller = GetComponent<CharacterBulletPower>();
        if(character_bullet_controller == null)
        {
            character_bullet_controller = this.gameObject.AddComponent<CharacterBulletPower>();
        }
    }


    public void ReceiveShot(int damage)
    {
        life = Mathf.Clamp(life - damage, 0, maxLife);
        if(life <= 0)
        {
            GameControllerManager.getGameControllerManager().EnemyDied(this.gameObject.name);
            Destroy(this.gameObject);
        }
    }

}
