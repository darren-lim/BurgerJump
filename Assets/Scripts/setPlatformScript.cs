using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPlatformScript : MonoBehaviour {

    private GameManagerScript script;

	// Use this for initialization
	void Awake ()
    {
        script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();
    }

    private void OnEnable()
    {
        float newX = Random.Range(-15, 15);
        float newY = script.platformPos;
        float newZ = Random.Range(15, 15);

        transform.position = new Vector3(newX, newY, newZ);
        script.addPlatformPos(10f);
    }
}
