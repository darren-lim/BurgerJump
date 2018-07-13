using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class N_EnemyScript : NetworkBehaviour {

    public GameObject ground;
    private Vector3 newPos;

    private float currX = 0f;
    private float currZ = 0f;

    private int randNum;
    public int speed;

    public bool started;

    private void Awake()
    {
        started = false;
        randNum = Random.Range(0, 21);
        speed = Random.Range(5,11);
    }

    private void Update()
    {
        if (randNum < 5)
        {
            transform.position = new Vector3(currX + PingPong(Time.time * speed, -20f, 20f), transform.position.y, transform.position.z);
        }
        else if(randNum < 10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, currZ + PingPong(Time.time * speed, -20f, 20f));
        }
        else
        {
            transform.position = new Vector3(currX + PingPong(Time.time * speed, -20f, 20f), transform.position.y, currZ + PingPong(Time.time * speed, -20f, 20f));
        }
    }

    private void OnEnable()
    {
        setNewPosition();
    }

    public void setNewPosition()
    {
        if (!started)
        {
            float newX = Random.Range(-35f, 35f);
            float newY = Random.Range(ground.transform.position.y + 300, ground.transform.position.y + 1000);
            float newZ = Random.Range(-35f, 35f);

            transform.position = new Vector3(newX, newY, newZ);
            currX = newX;
            currZ = newZ;
            started = true;
        }
        else
        {
            float newX = Random.Range(-35f, 35f);
            float newY = Random.Range(transform.position.y + 400, transform.position.y + 1100);
            float newZ = Random.Range(-35f, 35f);

            transform.position = new Vector3(newX, newY, newZ);
            currX = newX;
            currZ = newZ;
            randNum = Random.Range(0, 21);
            speed = Random.Range(5, 11);
        }
    }

    private float PingPong(float t, float minLength, float maxLength)
    {
        return Mathf.PingPong(t, maxLength - minLength) + minLength;
    }
}
