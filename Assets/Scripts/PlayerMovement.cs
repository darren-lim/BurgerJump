using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public float speed = 12f;
    public float jumpForce= 35f;
    private float jumpf;
    public float gravity = 30f;
    private Vector3 moveDir = Vector3.zero;
    CharacterController controller;

    private bool hasPowerUp = false;
    public float powerUpCooldown = 0f;

    public Text PowerUpText;

    private AudioSource jumpSound;
    public AudioSource powerUpSFX;

    void Start ()
    {
        controller = GetComponent<CharacterController>();
        jumpf = jumpForce;
        jumpSound = GetComponent<AudioSource>();
        PowerUpText.enabled = false;
    }
	
	void Update ()
    {
        Movement();
        checkPlatCollision();

        if(hasPowerUp && powerUpCooldown > 0f)
        {
            powerUpCooldown -= Time.deltaTime;
            float seconds = powerUpCooldown % 60;
            PowerUpText.text = "Power Jump! \n" + Mathf.RoundToInt(seconds).ToString() + " s";
        }
        else
        {
            jumpForce = jumpf;
            hasPowerUp = false;
            PowerUpText.enabled = false;
        }
        /*
        if (Input.GetKeyDown(KeyCode.T))
        {
            hasPowerUp = true;
            powerUpCooldown = 30f;
            jumpForce = 60f;
            PowerUpText.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            powerUpCooldown = 0f;
            jumpForce = jumpf;
            hasPowerUp = false;
        }*/
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
                jumpSound.Play();
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

    public void powerJump(float jump)
    {
        powerUpSFX.Play();
        if (hasPowerUp)
        {
            powerUpCooldown = 10f;
        }
        else
        {
            jumpForce += jump;
            powerUpCooldown = 10f;
            hasPowerUp = true;
            PowerUpText.enabled = true;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            collision.transform.SendMessage("startFalling", SendMessageOptions.DontRequireReceiver);
        }
    }
}
