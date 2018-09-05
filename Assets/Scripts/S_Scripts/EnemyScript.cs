using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    private GameObject player;
    private Vector3 newPos;

    private float currX;
    private float currZ;

    private int randNum;
    private int speed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Use this for initialization
    void Start()
    {
        float newX = Random.Range(-10f, 10f);
        float newY = Random.Range(player.transform.position.y + 100, player.transform.position.y + 600);
        float newZ = Random.Range(-10f, 10f);

        transform.position = new Vector3(newX, newY, newZ);
        currX = newX;
        currZ = newZ;
        randNum = Random.Range(0, 21);
        speed = Random.Range(5, 11);
    }

    private void Update()
    {
        if (randNum < 5)
        {
            transform.position = new Vector3(currX + PingPong(Time.time * speed, -13, 13), transform.position.y, transform.position.z);
        }
        else if(randNum < 10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, currZ + PingPong(Time.time * speed, -13, 13));
        }
        else
        {
            transform.position = new Vector3(currX + PingPong(Time.time * speed, -13, 13), transform.position.y, currZ + PingPong(Time.time * speed, -13, 13));
        }
    }

    private void OnDisable()
    {
        float newX = Random.Range(-10f, 10f);
        float newY = Random.Range(transform.position.y + 500, transform.position.y + 1100);
        float newZ = Random.Range(-10f, 10f);

        transform.position = new Vector3(newX, newY, newZ);
        currX = newX;
        currZ = newZ;
        randNum = Random.Range(0, 21);
        speed = Random.Range(5, 11);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Platform")
        {
            float newX = currX;// Random.Range(-5, 5);
            float newY = Random.Range(transform.position.y - 10, transform.position.y + 10);
            float newZ = currZ;// Random.Range(-5, 5);

            transform.position = new Vector3(newX, newY, newZ);
        }
        if (col.gameObject.tag == "player")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>().GameOver();
        }
    }

    private float PingPong(float t, float minLength, float maxLength)
    {
        return Mathf.PingPong(t, maxLength - minLength) + minLength;
    }
}
