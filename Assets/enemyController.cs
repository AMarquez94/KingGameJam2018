using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {

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

}
