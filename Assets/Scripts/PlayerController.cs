using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public enum States
    {
        IDLE,
        MOVING,
        DEAD
    }

    public GameObject health;
    public GameObject speed;
    public GameObject strength;
    public GameObject bullet_speed;
    public GameObject bullet_rate;
    public GameObject bullet_range;

    public States myState;
    public string playerName;

    public float probabilityRandomMutator;

    public enum PossibleMutations
    {
        NONE,
        BLACK_AND_WHITE,
        INVERSED_CONTROLS,
        INVERSED_MOUSE,
        SPRINT,
        NUMBER_MUTATIONS
        //SEE_BLURRED,
        //SLOWPROJS_TONDAMAGE,
        ////HOMMING_BULLETS,
        ////TWO_SHOTS,
        ////THREE_SHOTS,
        //TONCADENCY_FEWDAMAGE
    }

    public string mutationToString()
    {
        string myMutationName = "-";
        switch (myMutation)
        {
            case PossibleMutations.NONE:
                myMutationName = "-";
                break;
            case PossibleMutations.BLACK_AND_WHITE:
                myMutationName = "Black and White";
                break;
            case PossibleMutations.INVERSED_CONTROLS:
                myMutationName = "Inversed Controls";
                break;
            case PossibleMutations.INVERSED_MOUSE:
                myMutationName = "Inversed Aiming";
                break;
            case PossibleMutations.SPRINT:
                myMutationName = "Sprint";
                break;
            case PossibleMutations.NUMBER_MUTATIONS:
                myMutationName = "-";
                break;
        }
        return myMutationName;
    }

    public PossibleMutations myMutation;

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

    public Animator anim;

	// Use this for initialization
	void Start () {
        Init();
	}

    private void Init()
    {
        bulletMutation = new MutationController.BulletMutation();
        current_life = life;
        int player_respawns = GameControllerManager.getGameControllerManager().getPlayerRespawns();
        playerName = GameControllerManager.getGameControllerManager().getRandomPlayerName();

        int rangeValue = Random.Range(1, 3);
        GameObject hat = new GameObject();
        switch (rangeValue)
        {
            case 1:
                hat = Instantiate(Resources.Load("BoxHat") as GameObject);
                break;
            case 2:
                hat = Instantiate(Resources.Load("PyramidHat") as GameObject);
                break;
            case 3:
                hat = Instantiate(Resources.Load("SphereHat") as GameObject);
                break;
        }
        hat.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.grey);
        hat.transform.position = this.transform.position + new Vector3(0, 0.75f, 0);
        hat.transform.SetParent(this.transform);

        if (player_respawns > 0)
        {
            RandomMutator();
            Color finalColor = Color.white;
            finalColor.r = Random.Range(0f, 1f);
            finalColor.g = Random.Range(0f, 1f);
            finalColor.b = Random.Range(0f, 1f);
            this.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().materials[0].SetColor("_EmissionColor", finalColor);
            hat.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", finalColor);

        }
    }

    private void RandomMutator()
    {
        if (Random.value < probabilityRandomMutator)
        {
            int mutation = Random.Range(1, (int)PossibleMutations.NUMBER_MUTATIONS - 1);
            myMutation = (PossibleMutations)mutation;
        }
    }

    // Update is called once per frame
    void Update () {

        if(myState != States.DEAD)
        {
            #region --Control speed and movement--
            if (Input.GetKey(KeyCode.W))
            {
                if(myMutation != PossibleMutations.INVERSED_CONTROLS)
                {
                    current_speed_z = Mathf.Clamp(current_speed_z + incrSpeed, -maxSpeed, maxSpeed);
                }
                else
                {
                    current_speed_z = Mathf.Clamp(current_speed_z - incrSpeed, -maxSpeed, maxSpeed);
                }
            } else if (Input.GetKey(KeyCode.S))
            {
                if (myMutation != PossibleMutations.INVERSED_CONTROLS)
                {
                    current_speed_z = Mathf.Clamp(current_speed_z - incrSpeed, -maxSpeed, maxSpeed);
                }
                else
                {
                    current_speed_z = Mathf.Clamp(current_speed_z + incrSpeed, -maxSpeed, maxSpeed);
                }
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
                if (myMutation != PossibleMutations.INVERSED_CONTROLS)
                {
                    current_speed_x = Mathf.Clamp(current_speed_x + incrSpeed, -maxSpeed, maxSpeed);
                }
                else
                {
                    current_speed_x = Mathf.Clamp(current_speed_x - incrSpeed, -maxSpeed, maxSpeed);
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (myMutation != PossibleMutations.INVERSED_CONTROLS)
                {
                    current_speed_x = Mathf.Clamp(current_speed_x - incrSpeed, -maxSpeed, maxSpeed);
                }
                else
                {
                    current_speed_x = Mathf.Clamp(current_speed_x + incrSpeed, -maxSpeed, maxSpeed);
                }
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

            if(Input.GetKey(KeyCode.LeftShift) && myMutation == PossibleMutations.SPRINT)
            {
                transform.position += new Vector3(current_speed_x * vector_speed_normalized.x, 0, current_speed_z * vector_speed_normalized.z) * Time.deltaTime * 2f;
            }
            else
            {
                transform.position += new Vector3(current_speed_x * vector_speed_normalized.x, 0, current_speed_z * vector_speed_normalized.z) * Time.deltaTime;
            }
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
                Quaternion targetRotation;
                if(myMutation != PossibleMutations.INVERSED_MOUSE)
                {
                     targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                }
                else
                {
                    targetRotation = Quaternion.LookRotation(-targetPoint + transform.position);
                }

                // Smoothly rotate towards the target point.
                transform.rotation = targetRotation;
            }
            #endregion --Control rotation--

            #region --manage shootings--

            time_since_last_shoot += Time.deltaTime;
            canShoot = time_since_last_shoot > cadency ? true : false;

            if (Input.GetKey(KeyCode.Mouse0) && canShoot)
            {
                anim.Play("attack");
                canShoot = false;
                time_since_last_shoot = 0;
                GameObject bulletObject = Instantiate(bullet, this.transform.position + Vector3.up * 0.35f, Quaternion.identity);
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

        bullet_rate.GetComponent<Text>().text = cadency.ToString("0.00");
        bullet_speed.GetComponent<Text>().text = bullet.GetComponent<BulletController>().speed.ToString("0.00");
        bullet_range.GetComponent<Text>().text = bullet.GetComponent<BulletController>().range.ToString("0.00");
        strength.GetComponent<Text>().text = bullet.GetComponent<BulletController>().damage.ToString("0.00");
        speed.GetComponent<Text>().text = maxSpeed.ToString("0.00");
        health.GetComponent<Text>().text = current_life.ToString("0.0");
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
