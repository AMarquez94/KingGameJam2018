using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour {

    public float maxSpeed = 10f;
    public float speedIncrement = 0.5f;
    public float runningIncrementMultiplier = 2f;

    public float jumpForce = 10f;
    public float jumpAfterEnemyForce = 1f;

    float current_speed = 0;
    bool isGrounded = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float currentMaxSpeed = maxSpeed;
        float currentSpeedIncrement = speedIncrement;
        if (Input.GetKey(KeyCode.LeftShift)){
            currentMaxSpeed *= runningIncrementMultiplier;
            currentSpeedIncrement /= runningIncrementMultiplier;
        }

        if (Input.GetKey(KeyCode.A))
        {
            current_speed = Mathf.Clamp(current_speed - currentSpeedIncrement, -currentMaxSpeed, currentMaxSpeed);
        } else if (Input.GetKey(KeyCode.D))
        {
            current_speed = Mathf.Clamp(current_speed + currentSpeedIncrement, -currentMaxSpeed, currentMaxSpeed);
        }
        else
        {
            if(current_speed > 0)
            {
                current_speed = Mathf.Clamp(current_speed - speedIncrement, 0, current_speed);
            } else if(current_speed < 0)
            {
                current_speed = Mathf.Clamp(current_speed + speedIncrement, current_speed, 0);
            }
        }

        if (current_speed > 0)
        {
            transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(90, 180, 0));
        }
        else if (current_speed < 0)
        {
            transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(90, 180, -180));
        }

        transform.position += new Vector3(1f,0f,0f) * current_speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Rigidbody myRigidbody = GetComponent<Rigidbody>();
            myRigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(Physics.gravity * rigidbody.mass);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
    }
}
