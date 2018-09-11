using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : ChangePlatformPosition {

    public float jumpPower = 20f;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Use this for initialization
    void Start ()
    {
        setNewPosition(15f, player.transform.position.y + 300, player.transform.position.y + 800);
    }

    private void OnDisable ()
    {
        if (player == null) return;
        setNewPosition(15f, player.transform.position.y + 700, player.transform.position.y + 1300);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Platform")
        {
            setNewPosition(15f, player.transform.position.y - 10, player.transform.position.y + 10);
        }
        if(col.gameObject.tag == "player")
        {
            col.gameObject.GetComponent<PlayerMovement>().powerJump(jumpPower);
            this.gameObject.SetActive(false);
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
