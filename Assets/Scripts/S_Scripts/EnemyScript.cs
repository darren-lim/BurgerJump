using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : ChangePlatformPosition {

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
        // sets initial positions
        setNewPosition(15f, player.transform.position.y + 100, player.transform.position.y + 600);
        currX = transform.position.x;
        currZ = transform.position.z;
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

    //sets next position
    private void OnDisable()
    {
        if (player == null) return;
        setNewPosition(15f, player.transform.position.y + 500, player.transform.position.y + 1100);
        currX = transform.position.x;
        currZ = transform.position.z;
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

    //bounces between two points using Mathf.PingPong, but returns middle of the points.
    private float PingPong(float t, float minLength, float maxLength)
    {
        return Mathf.PingPong(t, maxLength - minLength) + minLength;
    }

    public override void setNewPosition(float maxXZ, float minY, float maxY)
    {
        float newX = Random.Range(-maxXZ, maxXZ);
        float newY = Random.Range(minY, maxY);
        float newZ = Random.Range(-maxXZ, maxXZ);

        transform.position = new Vector3(newX, newY, newZ);
    }
}
