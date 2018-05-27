using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreateScript : MonoBehaviour {

    //player jumps 10 units. range y is from 5 - 20 units.
    //range on z and x axis is 10 to 10

    //for enemy use mathf.pingpong

   // public float minY = 5;
    private GameObject player;
    public bool fall = false;

    public Color startColor = Color.white;
    public Color endColor = Color.red;
    public float duration = 7f;
    public Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        float newX = Random.Range(-15, 15);
        float newY = Random.Range(player.transform.position.y + 2, player.transform.position.y + 230);
        float newZ = Random.Range(-15, 15);

        transform.position = new Vector3(newX, newY, newZ);
    }

    void OnDisable ()
    {
        if (player == null) return;
        if (fall)
        {
            StopAllCoroutines();
            float newX = Random.Range(-15, 15);
            float newY = Random.Range(player.transform.position.y + 70, player.transform.position.y + 110);
            float newZ = Random.Range(-15, 15);

            transform.position = new Vector3(newX, newY, newZ);
        }
        else
        {
            float newX = Random.Range(-15, 15);
            float newY = Random.Range(transform.position.y + 50, transform.position.y + 150);
            float newZ = Random.Range(-15, 15);

            transform.position = new Vector3(newX, newY, newZ);
        }
        rend.material.color = startColor;
        fall = false;
    }

    private void OnEnable()
    {
        float willFall = Random.Range(0, 100);
        if (transform.position.y > 100f && willFall > 50)
        {
            fall = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")// && fall == true)
        {
            StartCoroutine("ChangeColor");
            Debug.Log("FALLING");
        }
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
        while (transform.position.y > 10f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (Time.deltaTime * 5), transform.position.z);
            yield return null;
        }
    }
}
