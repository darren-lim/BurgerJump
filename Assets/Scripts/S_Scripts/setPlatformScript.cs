using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPlatformScript : ChangePlatformPosition {

    [SerializeField]
    private GameManagerScript GameScript;

	// Use this for initialization
	void Awake ()
    {
        GameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();
    }

    private void OnEnable()
    {
        Vector3 Pos = setNewPosition(15f, GameScript.platformPos, GameScript.platformPos);
        transform.position = Pos;
        GameScript.addPlatformPos(10f);
    }

    public override Vector3 setNewPosition(float maxXZ, float minY, float maxY)
    {
        float newX = Random.Range(-maxXZ, maxXZ);
        float newY = maxY;
        float newZ = Random.Range(-maxXZ, maxXZ);

        return new Vector3(newX, newY, newZ);
    }
}
