using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float damage = 10.0f;
    public float speed = 3.0f;
    public float range = 10.0f;

    public float minDamage;
    public float maxDamage;
    public float minSpeed;
    public float maxSpeed;
    public float minRange;
    public float maxRange;

    private float ammountMoved = 0.0f;
    private Vector3 lastFramePosition;
    private GameObject sender;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastFramePosition = this.transform.position;
        audioSource.Play();
    }

    private void Update()
    {
        ammountMoved += (this.transform.position - lastFramePosition).magnitude;
        if(ammountMoved >= range)
        {
            destroyBullet();
        }
        lastFramePosition = this.transform.position; 
    }

    public void SetSender(GameObject sender)
    {
        this.sender = sender;
    }

    public GameObject GetSender()
    {
        return this.sender;
    }

    private void destroyBullet()
    {
        Destroy(this.gameObject);
    }

    public void SetMutation(MutationController.BulletMutation bulletMutation)
    {
        damage = Mathf.Clamp(damage + bulletMutation.bulletDamageModifier, minDamage, maxDamage);
        speed = Mathf.Clamp(speed + bulletMutation.bulletSpeedModificer, minSpeed, maxSpeed);
        range = Mathf.Clamp(range + bulletMutation.bulletRangeModifier, minRange, maxRange);
    }

    public void setVariables(float new_damage, float new_speed, float new_range)
    {
        damage = new_damage;
        speed = new_speed;
        range = new_range;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && sender != null && sender.tag != "Player")
        {
            /* Hit the player and destroy the bullet */
            other.gameObject.GetComponent<PlayerController>().ReceiveShot((int)damage);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Enemy" && sender != null && sender.tag != "Enemy")
        {
            /* Hit the enemy and destroy the bullet */
            other.gameObject.GetComponent<enemyController>().ReceiveShot((int)damage);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Obstacle")
        {
            /* Hit the an obstacle and destroy the bullet */
            Destroy(this.gameObject);
        }
    }
}
