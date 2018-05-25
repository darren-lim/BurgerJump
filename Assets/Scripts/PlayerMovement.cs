using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 7f;
    public float jumpForce= 15f;
    public float gravity = 20f;
    private Vector3 moveDir = Vector3.zero;
    CharacterController controller;

    void Start ()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }
	
	void Update ()
    {
        Movement();
        checkPlatCollision();

        if (transform.position.y < -10)
        {

        }
	}

    public void Movement()
    {
        if (controller.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = Vector3.ClampMagnitude(moveDir, 1);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;
            if (Input.GetButtonDown("Jump"))
            {
                moveDir.y = jumpForce;
            }
        }
        else
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), moveDir.y, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir.x *= speed;
            moveDir.z *= speed;
        }

        moveDir.y -= gravity * Time.deltaTime;

        controller.Move(moveDir * Time.deltaTime);
    }

    public void checkPlatCollision()
    {
        //move the player to another layer to pass through the platforms
        if(moveDir.y > 0)
        {
            gameObject.layer = 9;
        }
        else
        {
            gameObject.layer = 0;
        }
    }
}
