using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class N_setPlatformScript : NetworkBehaviour {

    [SerializeField]
    private N_GameManagerScript script;

    private void OnEnable()
    {
        float newX = Random.Range(-15f, 15f);
        float newY = script.getPlatformPos();
        float newZ = Random.Range(-15f, 15f);
        transform.position = new Vector3(newX, newY, newZ);
    }
}
