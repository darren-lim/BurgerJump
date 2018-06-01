using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    //player jumps 10 units. range y is from 5 - 20 units.
    //range on z and x axis is 10 to 10

    //for enemy use mathf.pingpong

   // public float minY = 5;
    private GameObject player;
    public bool fall = false; //it will fall
    public bool fell = false; //it already fell

    public Color startColor = Color.white;
    public Color endColor = Color.red;
    public float duration = 10f;
    public Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("player");
    }

    private void Start()
    {
        float newX = Random.Range(-20, 20);
        float newY = Random.Range(player.transform.position.y + 5, player.transform.position.y + 300);
        float newZ = Random.Range(-20, 20);

        transform.position = new Vector3(newX, newY, newZ);
    }

    void OnDisable ()
    {
        if (player == null) return;
        if (fell)
        {
            StopAllCoroutines();
            float newX = Random.Range(-20, 20);
            float newY = Random.Range(player.transform.position.y + 100, player.transform.position.y + 200);
            float newZ = Random.Range(-20, 20);

            transform.position = new Vector3(newX, newY, newZ);
        }
        else
        {
            float newX = Random.Range(-20, 20);
            float newY = Random.Range(transform.position.y + 290, transform.position.y + 310);
            float newZ = Random.Range(-20, 20);

            transform.position = new Vector3(newX, newY, newZ);
        }
        rend.material.color = startColor;
        fall = false;
        fell = false;
    }

    private void OnEnable()
    {
        float willFall = Random.Range(0, 110);
        if (transform.position.y > 800f && willFall >= 10) fall = true;
        else if (transform.position.y > 500f && willFall >= 30) fall = true;
        else if (transform.position.y > 300f && willFall >= 60) fall = true;
        else if (transform.position.y > 100f && willFall >= 80) fall = true;
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player") { Debug.Log("BOOM"); }
        if (collision.gameObject.name == "Player" && fall == true)
        {
            StartCoroutine("ChangeColor");
            Debug.Log("FALLING");
        }
    }
    */
    public void startFalling()
    {
        if(fall == true)
            StartCoroutine("ChangeColor");
    }
    
    IEnumerator ChangeColor()
    {
        for(float i = 0.01f; i < duration; i+=0.1f)
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
