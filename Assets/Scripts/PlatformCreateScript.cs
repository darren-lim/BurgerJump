using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreateScript : MonoBehaviour {

    //player jumps 10 units. range y is from 5 - 20 units.
    //range on z and x axis is 10 to 10

    public float minY = 5;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        float newX = Random.Range(-15, 15);
        float newY = Random.Range(player.transform.position.y + 2, player.transform.position.y + 250);
        float newZ = Random.Range(-15, 15);

        transform.position = new Vector3(newX, newY, newZ);
    }

    void OnDisable ()
    {
        if (player == null) return;
        float newX = Random.Range(-15, 15);
        float newY = Random.Range(transform.position.y + 80, transform.position.y + 100);
        float newZ = Random.Range(-15, 15);

        transform.position = new Vector3(newX, newY, newZ);
	}

}
