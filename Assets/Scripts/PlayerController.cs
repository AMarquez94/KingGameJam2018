using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum States
    {
        IDLE,
        MOVING,
        DEAD
    }

    public States myState;

    public float maxSpeed;
    public float incrSpeed;
    public float cadency;
    public int life;

    private int current_life;
    private float current_speed_x;
    private float current_speed_z;

    private float time_since_last_shoot;
    bool canShoot = true;

    public float minSpeed, maxMSpeed;
    public float minCadency, maxCadency;
    public int minLife, maxLife;

    public GameObject bullet;

    public MutationController.BulletMutation bulletMutation;

	// Use this for initialization
	void Start () {
        bulletMutation = new MutationController.BulletMutation();
        current_life = life;
	}
	
	// Update is called once per frame
	void Update () {

        if(myState != States.DEAD)
        {
            #region --Control speed and movement--
            if (Input.GetKey(KeyCode.W))
            {
                current_speed_z = Mathf.Clamp(current_speed_z + incrSpeed, -maxSpeed, maxSpeed);
            } else if (Input.GetKey(KeyCode.S))
            {
                current_speed_z = Mathf.Clamp(current_speed_z - incrSpeed, -maxSpeed, maxSpeed);
            }
            else
            {
                if (current_speed_z > 0)
                {
                    current_speed_z = Mathf.Clamp(current_speed_z - incrSpeed, 0, current_speed_z);
                }
                else if (current_speed_z < 0)
                {
                    current_speed_z = Mathf.Clamp(current_speed_z + incrSpeed, current_speed_z, 0);
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                current_speed_x = Mathf.Clamp(current_speed_x + incrSpeed, -maxSpeed, maxSpeed);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                current_speed_x = Mathf.Clamp(current_speed_x - incrSpeed, -maxSpeed, maxSpeed);
            }
            else
            {
                if (current_speed_x > 0)
                {
                    current_speed_x = Mathf.Clamp(current_speed_x - incrSpeed, 0, current_speed_x);
                }
                else if (current_speed_x < 0)
                {
                    current_speed_x = Mathf.Clamp(current_speed_x + incrSpeed, current_speed_x, 0);
                }
            }

            Vector3 vector_speed_normalized = new Vector3(Mathf.Abs(current_speed_x), 0, Mathf.Abs(current_speed_z));
            vector_speed_normalized.Normalize();
            transform.position += new Vector3(current_speed_x * vector_speed_normalized.x, 0, current_speed_z * vector_speed_normalized.z) * Time.deltaTime;
            #endregion --Control speed and movement--

            #region --Control rotation--
            // Generate a plane that intersects the transform's position with an upwards normal.
            Plane playerPlane = new Plane(Vector3.up, transform.position);

            // Generate a ray from the cursor position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float hitdist = 0.0f;
            // If the ray is parallel to the plane, Raycast will return false.
            if (playerPlane.Raycast(ray, out hitdist))
            {
                // Get the point along the ray that hits the calculated distance.
                Vector3 targetPoint = ray.GetPoint(hitdist);

                // Determine the target rotation.  This is the rotation if the transform looks at the target point.
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

                // Smoothly rotate towards the target point.
                transform.rotation = targetRotation;
            }
            #endregion --Control rotation--

            #region --manage shootings--

            time_since_last_shoot += Time.deltaTime;
            canShoot = time_since_last_shoot > cadency ? true : false;

            if (Input.GetKey(KeyCode.Mouse0) && canShoot)
            {
                canShoot = false;
                time_since_last_shoot = 0;
                GameObject bulletObject = Instantiate(bullet, this.transform.position, Quaternion.identity);
                bulletObject.transform.forward = this.transform.forward;
                bulletObject.GetComponent<BulletController>().SetSender(this.gameObject);
            }

            #endregion --manage shootings--
            
            #region --manage states--
            if (current_life <= 0)
            {
                myState = States.DEAD;
            }
            if (current_speed_x == 0 && current_speed_z == 0)
            {
                myState = States.IDLE;
            }
            else
            {
                myState = States.MOVING;
            }
            #endregion --manage states--
        }
    }

    private bool MotionKeysNotPressed()
    {
        return !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void SetModifier(MutationController.PlayerMutation playerMutation, MutationController.BulletMutation bulletMutation)
    {
        print("Player mutation " + playerMutation.lifeModifier + " - " + playerMutation.speedModifier + " - " + playerMutation.cadencyModifier);
        life = Mathf.Clamp(life + playerMutation.lifeModifier, minLife, maxLife);
        maxSpeed = Mathf.Clamp(maxSpeed + playerMutation.speedModifier, minSpeed, maxMSpeed);
        cadency = Mathf.Clamp(cadency + playerMutation.cadencyModifier, minCadency, maxCadency);

        print("Bullet mutation " + bulletMutation.bulletDamageModifier + " - " + bulletMutation.bulletRangeModifier + " - " + bulletMutation.bulletSpeedModificer);
        this.bulletMutation.bulletDamageModifier += bulletMutation.bulletDamageModifier;
        this.bulletMutation.bulletRangeModifier += bulletMutation.bulletRangeModifier;
        this.bulletMutation.bulletSpeedModificer += bulletMutation.bulletSpeedModificer;
    }

    public void ReceiveShot(int damage)
    {
        this.current_life = Mathf.Clamp(current_life - damage, 0, maxLife);
        if(current_life <= 0)
        {
            myState = States.DEAD;
        }
    }
}
