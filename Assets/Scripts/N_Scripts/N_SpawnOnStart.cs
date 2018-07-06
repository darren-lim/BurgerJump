using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class N_SpawnOnStart : NetworkBehaviour {

    public GameObject setPlatPooler;
    //public GameObject platPooler;

    public override void OnStartServer()
    {
        setPlatPooler.SetActive(true);
        //platPooler.SetActive(true);
        base.OnStartServer();
    }
}
