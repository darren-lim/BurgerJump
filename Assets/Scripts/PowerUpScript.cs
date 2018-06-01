using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour {

    public float jumpPower = 20f;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Use this for initialization
    void Start ()
    {
        float newX = Random.Range(-10, 10);
        float newY = Random.Range(player.transform.position.y + 100, player.transform.position.y + 800);
        float newZ = Random.Range(-10, 10);

        transform.position = new Vector3(newX, newY, newZ);
    }

    private void OnDisable ()
    {
        float newX = Random.Range(-10, 10);
        float newY = Random.Range(transform.position.y + 400, transform.position.y + 900);
        float newZ = Random.Range(-10, 10);

        transform.position = new Vector3(newX, newY, newZ);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Platform")
        {
            float newX = Random.Range(-10, 10);
            float newY = Random.Range(transform.position.y - 10, transform.position.y + 10);
            float newZ = Random.Range(-10, 10);

            transform.position = new Vector3(newX, newY, newZ);
        }
        if(col.gameObject.tag == "player")
        {
            col.gameObject.GetComponent<PlayerMovement>().powerJump(jumpPower);
            this.gameObject.SetActive(false);
        }
    }
}
