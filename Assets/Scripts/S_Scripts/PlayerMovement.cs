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

    private float yPos;
    private bool grounded = true;

    void Start ()
    {
        controller = GetComponent<CharacterController>();
        jumpf = jumpForce;
        jumpSound = GetComponent<AudioSource>();
        PowerUpText.enabled = false;
        yPos = transform.position.y;
    }
	
	void Update ()
    {
        Movement();
        checkPlatCollision();
        //check if we are grounded
        if (grounded && transform.position.y < yPos)
        {
            grounded = false;
        }
        //check if we have powerup. If we do, then put UI cooldown on screen
        if (hasPowerUp && powerUpCooldown > 0f)
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

    //moves character controller.
    public void Movement()
    {
        controller.Move(moveDir * Time.deltaTime);
        if (grounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = Vector3.ClampMagnitude(moveDir, 1);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;
            if (Input.GetButtonDown("Jump"))
            {
                moveDir.y = jumpForce;
                grounded = false;
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
        //give player powerup for 10 seconds
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
        if (collision.gameObject.tag == "Platform" && (collision.gameObject.transform.position.y + 1.2f) < this.transform.position.y)
        {
            //send a mesage to platform to fall. Platform falls if it is willing to fall.
            collision.transform.SendMessage("startFalling", SendMessageOptions.DontRequireReceiver);
            grounded = true;
            yPos = transform.position.y;
        }
        else if ((collision.gameObject.tag == "SetPlatforms" && (collision.gameObject.transform.position.y + 1.2f) < this.transform.position.y) || collision.gameObject.tag == "ground")
        {
            grounded = true;
            yPos = transform.position.y;
        }

    }
}
