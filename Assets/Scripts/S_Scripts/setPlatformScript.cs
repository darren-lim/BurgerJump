using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPlatformScript : MonoBehaviour {

    [SerializeField]
    private GameManagerScript GameScript;

	// Use this for initialization
	void Awake ()
    {
        GameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();
    }

    private void OnEnable()
    {
        float newX = Random.Range(-15f, 15f);
        float newY = GameScript.platformPos;
        float newZ = Random.Range(-15f, 15f);

        transform.position = new Vector3(newX, newY, newZ);
        GameScript.addPlatformPos(10f);
    }
}
