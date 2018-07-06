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

    //[SyncVar]
    //private Vector3 syncPos;

    private AudioSource fallingSound;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        ground = GameObject.FindGameObjectWithTag("ground");
        fallingSound = GetComponent<AudioSource>();
        //syncPos = GetComponent<Transform>().position;
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
        if (!started)
        {
            float newX = Random.Range(-20f, 20f);
            float newY = Random.Range(ground.transform.position.y + 10, ground.transform.position.y + 350);
            float newZ = Random.Range(-20f, 20f);

            transform.position = new Vector3(newX, newY, newZ);
            started = true;
        }
        //if (ground == null) return;
        else if (fell)
        {
            fallingSound.Stop();
            StopAllCoroutines();
            float newX = Random.Range(-20f, 20f);
            float newY = Random.Range(ground.transform.position.y + 630, ground.transform.position.y + 660);
            float newZ = Random.Range(-20f, 20f);

            transform.position = new Vector3(newX, newY, newZ);
        }
        else
        {
            float newX = Random.Range(-20f, 20f);
            float newY = Random.Range(transform.position.y + 330, transform.position.y + 360);
            float newZ = Random.Range(-20f, 20f);

            transform.position = new Vector3(newX, newY, newZ);
        }
        rend.material.color = startColor;
        fall = false;
        fell = false;
        //if(isServer) RpcSendPos();
    }

    private void OnDisable()
    {
        float willFall = Random.Range(0, 110);
        if (transform.position.y > 1500f && willFall >= 25) fall = true;
        else if (transform.position.y > 900f && willFall >= 40) fall = true;
        else if (transform.position.y > 500f && willFall >= 65) fall = true;
        else if (transform.position.y > 100f && willFall >= 90) fall = true;
    }
    /*
    [ClientRpc]
    void RpcSendPos()
    {
        syncPos = transform.position;
    }*/

    public void startFalling()
    {
        if (fall == true)
            StartCoroutine("ChangeColor");
    }

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

    IEnumerator Fall()
    {
        fell = true;
        while (transform.position.y > 10f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime, transform.position.z);
            yield return null;
        }
    }
}
