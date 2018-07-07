using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class N_setPlatformScript : NetworkBehaviour {

    //[SerializeField]
    public N_GameManagerScript GameScript;

    //[SyncVar]
    //private Vector3 syncPos;

	// Use this for initialization
	void Awake ()
    {
        //GameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<N_GameManagerScript>();
        //syncPos = GetComponent<Transform>().position;
    }

    private void OnEnable()
    {
        //if (!isServer) return;
        float newX = Random.Range(-15f, 15f);
        float newY = GameScript.getPlatformPos();
        float newZ = Random.Range(-15f, 15f);

        transform.position = new Vector3(newX, newY, newZ);
        GameScript.addPlatformPos(10f);

        //if (isServer) RpcSendPos();
    }
    /*
    [ClientRpc]
    void RpcSendPos()
    {
        syncPos = transform.position;
    }*/
}
