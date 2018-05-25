using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreateScript : MonoBehaviour {

    //player jumps 10 units. range y is from 5 - 20 units.
    //range on z and x axis is 10 to 10

    public float minY = 5;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float newX = Random.Range(-10, 10);
        float newY = Random.Range(player.transform.position.y + 2, player.transform.position.y + 80);
        float newZ = Random.Range(-10, 10);

        transform.position = new Vector3(newX, newY, newZ);
    }

    void OnDisable ()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float newX = Random.Range(-10, 10);
        float newY = Random.Range(player.transform.position.y + 50, player.transform.position.y + 100);
        float newZ = Random.Range(-10, 10);

        transform.position = new Vector3(newX, newY, newZ);
	}

}
