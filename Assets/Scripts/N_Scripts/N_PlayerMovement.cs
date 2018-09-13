using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class N_PlayerMovement : NetworkBehaviour {

    public float speed = 12f;
    public float jumpForce= 35f;
    private float jumpf;
    public float gravity = 30f;
    private Vector3 moveDir = Vector3.zero;
    CharacterController controller;

    private bool hasPowerUp = false;
    public float powerUpCooldown = 0f;

    private float yPos;
    public bool grounded = true;

    [SerializeField]
    Camera cam;

    public GameObject ground;

    private N_Player playerScript;

    [SerializeField]
    private N_GameManagerScript gameManager;

    //public Text PowerUpText;

    private AudioSource jumpSound;
    private AudioSource powerUpSFX;

    void Start()
    {
        playerScript = GetComponent<N_Player>();
        controller = GetComponent<CharacterController>();
        jumpf = jumpForce;
        yPos = transform.position.y;
        jumpSound = GetComponent<AudioSource>();
        powerUpSFX = GameObject.FindGameObjectWithTag("PowerUpAudio").GetComponent<AudioSource>();
    }
	
	void Update ()
    {
        if (isLocalPlayer)
        {
            if(controller.enabled)
                Movement();
            checkPlatCollision();
            if(grounded && transform.position.y < yPos - 0.01f)
            {
                grounded = false;
            }
            if (hasPowerUp && powerUpCooldown > 0f)
            {
                powerUpCooldown -= Time.deltaTime;
            }
            else
            {
                jumpForce = jumpf;
                hasPowerUp = false;
            }
            if (transform.position.y < ground.transform.position.y)
            {
                if (isLocalPlayer)
                {
                    if (!gameManager.gameStart)
                    {
                        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                    }
                    else
                    {
                        playerScript.setSpectatingTrue();
                        if (isServer)
                            RpcIsSpectator();
                        else
                            CmdIsSpectator();
                        GetComponent<N_SpectateCam>().enabled = true;
                    }
                }
            }
        }
    }

    public void Movement()
    {
        controller.Move(moveDir * Time.deltaTime);
        if (N_PauseMenu.isOn)
        {
            moveDir.y -= gravity * Time.deltaTime;
            return;
        }
        //controller.Move(moveDir * Time.deltaTime);
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
        //controller.Move(moveDir * Time.deltaTime);
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
            playerScript.setPowerUpTime(powerUpCooldown);
        }
        else
        {
            jumpForce += jump;
            powerUpCooldown = 10f;
            playerScript.setPowerUpTime(powerUpCooldown);
            hasPowerUp = true;
        }
    }


    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.gameObject.tag == "Platform" && (collision.gameObject.transform.position.y + 1f) < this.transform.position.y)
        {
            collision.transform.SendMessage("startFalling", SendMessageOptions.DontRequireReceiver);
            grounded = true;
            yPos = transform.position.y;
        }
        else if(((collision.gameObject.tag == "SetPlatforms" || collision.gameObject.tag == "SetPlatforms2") && (collision.gameObject.transform.position.y + 1f) < this.transform.position.y) || collision.gameObject.tag == "ground")
        {
            grounded = true;
            yPos = transform.position.y;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "ground" || collider.gameObject.tag == "Enemy")
        {
            if (isLocalPlayer)
            {
                playerScript.setSpectatingTrue();
                if (isServer)
                    RpcIsSpectator();
                else
                    CmdIsSpectator();
                GetComponent<N_SpectateCam>().enabled = true;
            }
        }
    }

    [Command]
    public void CmdIsSpectator()
    {
        RpcIsSpectator();
    }

    [ClientRpc]
    void RpcIsSpectator()
    {
        this.gameObject.tag = "Spectator";
    }
    
}
