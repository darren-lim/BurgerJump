using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class N_PlatformScript : NetworkBehaviour {

    //[SerializeField]
    public GameObject ground;
    [SyncVar]
    public bool fall = false; //it will fall
    [SyncVar]
    public bool fell = false; //it already fell
    [SyncVar]
    bool started = false;

    public Color startColor = Color.gray;
    public Color endColor = Color.red;
    public float duration = 40f;
    public Renderer rend;

    private AudioSource fallingSound;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        fallingSound = GetComponent<AudioSource>();
    }
    /*
    private void Start()
    {
        float newX = Random.Range(-20f, 20f);
        float newY = Random.Range(ground.transform.position.y + 10, ground.transform.position.y + 350);
        float newZ = Random.Range(-20f, 20f);

        transform.position = new Vector3(newX, newY, newZ);
        //if (isServer) RpcSendPos();
    }*/

    void OnEnable()
    {
        setNewPosition();
    }

    public void setNewPosition()
    {
        if (!started)
        {
            //set platform position when game starts
            float newX = Random.Range(-45f, 45f);
            float newY = Random.Range(ground.transform.position.y + 10, ground.transform.position.y + 600);
            float newZ = Random.Range(-45f, 45f);

            transform.position = new Vector3(newX, newY, newZ);
            started = true;
            return;
        }
        //if (ground == null) return;
        else if (fell)
        {
            fallingSound.Stop();
            StopAllCoroutines();
            float newX = Random.Range(-45f, 45f);
            float newY = Random.Range(ground.transform.position.y + 730, ground.transform.position.y + 960);
            float newZ = Random.Range(-45f, 45f);

            transform.position = new Vector3(newX, newY, newZ);
        }
        else
        {
            //set platform position when ground touches platform
            float newX = Random.Range(-45f, 45f);
            float newY = Random.Range(transform.position.y + 480, transform.position.y + 560);
            float newZ = Random.Range(-45f, 45f);

            transform.position = new Vector3(newX, newY, newZ);
        }
        rend.material.color = startColor;
        fall = false;
        fell = false;
        RpcSendFall(false);
        RpcSendFell(false);

        setFallingChance();
    }

    void setFallingChance()
    {
        //set platform fall chance
        float willFall = Random.Range(0, 110);
        if (transform.position.y > 1700f && willFall >= 25)
        {
            RpcSendFall(true);
            fall = true;
        }
        else if (transform.position.y > 1000f && willFall >= 40)
        {
            RpcSendFall(true);
            fall = true;
        }
        else if (transform.position.y > 600f && willFall >= 65)
        {
            RpcSendFall(true);
            fall = true;
        }
        else if (transform.position.y > 300f && willFall >= 90)
        {
            RpcSendFall(true);
            fall = true;
        }
    }

    public void startFalling()
    {
        if (fall == true)
            StartCoroutine("ChangeColor");
    }

    //changes color to red to indicate the platform will fall
    IEnumerator ChangeColor()
    {
        fallingSound.Play();
        for (float i = 0.01f; i < duration; i += 0.1f)
        {
            rend.material.color = Color.Lerp(startColor, endColor, i / duration);
            yield return null;
        }
        StartCoroutine("Fall");
    }

    //Sets platform position to fall
    IEnumerator Fall()
    {
        RpcSendFell(true);
        fell = true;
        while (transform.position.y > 10f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime, transform.position.z);
            yield return null;
        }
    }

    [ClientRpc]
    void RpcSendFall(bool f)
    {
        fall = f;
    }

    [ClientRpc]
    void RpcSendFell(bool f)
    {
        fell = f;
    }
}
