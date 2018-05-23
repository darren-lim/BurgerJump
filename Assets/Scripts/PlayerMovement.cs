using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 7f;
    public float jumpForce= 10f;
    public float gravity = 20f;
    private Vector3 moveDir = Vector3.zero;
    CharacterController controller;

    void Start ()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }
	
	void Update ()
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
        moveDir.y -= gravity * Time.deltaTime;

        controller.Move(moveDir * Time.deltaTime);
	}
}
