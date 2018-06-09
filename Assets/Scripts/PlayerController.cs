using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float maxSpeed;
    public float incrSpeed;
    public float cadency;
    public int life;


    private int current_life;
    private float current_speed_x;
    private float current_speed_z;

    public GameObject bullet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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


        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(0f, -angle, 0f));
    }

    private bool MotionKeysNotPressed()
    {
        return !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void SetModifier(MutationController.PlayerMutation mutation)
    {
        print("Player mutation " + mutation.lifeModifier + " - " + mutation.speedModifier + " - " + mutation.cadencyModifier);
        life = Mathf.Clamp(life + mutation.lifeModifier, 10, 300);
        maxSpeed = Mathf.Clamp(maxSpeed + mutation.speedModifier, 1f, 20f);
        cadency = Mathf.Clamp(cadency + mutation.cadencyModifier, 1f, 20f);
    }
}
