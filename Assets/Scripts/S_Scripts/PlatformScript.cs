using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : ChangePlatformPosition {

    //player jumps 10 units. range y is from 5 - 20 units.
    //range on z and x axis is 10 to 10

    //for enemy use mathf.pingpong

    // public float minY = 5;
    private GameObject player;
    public bool fall = false; //it will fall
    public bool fell = false; //it already fell

    public Color startColor = Color.gray;
    public Color endColor = Color.red;
    public float duration = 40f;
    public Renderer rend;

    private AudioSource fallingSound;

    private Vector3 scale;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("player");
        fallingSound = GetComponent<AudioSource>();
    }
    
    private void Start()
    {
        setNewPosition(27f, player.transform.position.y + 5, player.transform.position.y + 450);
        scale = new Vector3(3.5f, 0.5f, 3.5f);
    }

    void OnDisable()
    {
        fallingSound.Stop();
        if (player == null) return;
        if (fell)
        {
            //reset position above the player if the platform fell
            StopAllCoroutines();
            setNewPosition(27f, player.transform.position.y + 100, player.transform.position.y + 200);
        }
        else
        {
            //if platform is disabled, reset position above original position
            setNewPosition(27f, transform.position.y + 340, transform.position.y + 360);
        }
        rend.material.color = startColor;
        fall = false;
        fell = false;
    }

    private void OnEnable()
    {
        //set new falling chance
        float willFall = Random.Range(0, 110);
        if (transform.position.y > 1000f && willFall >= 25) fall = true;
        else if (transform.position.y > 700f && willFall >= 40) fall = true;
        else if (transform.position.y > 400f && willFall >= 65) fall = true;
        else if (transform.position.y > 100f && willFall >= 90) fall = true;

        if(transform.position.y > 1100f && transform.localScale != scale)
        {
            transform.localScale = scale;
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
        fell = true;
        while (transform.position.y > 10f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime, transform.position.z);
            yield return null;
        }
    }

    public override void setNewPosition(float maxXZ, float minY, float maxY)
    {
        float newX = Random.Range(-maxXZ, maxXZ);
        float newY = Random.Range(minY, maxY);
        float newZ = Random.Range(-maxXZ, maxXZ);

        transform.position = new Vector3(newX, newY, newZ);
    }
}
