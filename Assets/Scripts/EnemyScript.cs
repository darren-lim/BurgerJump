using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    private GameObject player;
    private Vector3 newPos;

    private int randNum;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Use this for initialization
    void Start()
    {
        float newX = Random.Range(-10, 10);
        float newY = Random.Range(player.transform.position.y + 200, player.transform.position.y + 600);
        float newZ = Random.Range(-10, 10);

        transform.position = new Vector3(newX, newY, newZ);

        randNum = Random.Range(0, 10);
    }

    private void Update()
    {
        if (randNum > 5)
        {
            transform.position = new Vector3(PingPong(Time.time * 8, -10, 10), transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, PingPong(Time.time * 8, -10, 10));
        }
    }

    private void OnDisable()
    {
        float newX = Random.Range(-10, 10);
        float newY = Random.Range(transform.position.y + 600, transform.position.y + 1000);
        float newZ = Random.Range(-10, 10);

        transform.position = new Vector3(newX, newY, newZ);

        randNum = Random.Range(0, 10);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Platform")
        {
            float newX = 0;// Random.Range(-5, 5);
            float newY = Random.Range(transform.position.y - 10, transform.position.y + 10);
            float newZ = 0;// Random.Range(-5, 5);

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
