using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPlatformScript : ChangePlatformPosition {

    [SerializeField]
    private GameManagerScript GameScript;

    private Vector3 scale;

    // Use this for initialization
    void Awake ()
    {
        GameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();
    }

    private void Start()
    {
        scale = new Vector3(3.5f, 0.5f, 3.5f);
    }

    private void OnEnable()
    {
        setNewPosition(15f, GameScript.platformPos, GameScript.platformPos);
        GameScript.addPlatformPos(10f);

        if (transform.position.y > 1100f && transform.localScale != scale)
        {
            transform.localScale = scale;
        }
    }

    public override void setNewPosition(float maxXZ, float minY, float maxY)
    {
        float newX = Random.Range(-maxXZ, maxXZ);
        float newY = maxY;
        float newZ = Random.Range(-maxXZ, maxXZ);

        transform.position = new Vector3(newX, newY, newZ);
    }
}
