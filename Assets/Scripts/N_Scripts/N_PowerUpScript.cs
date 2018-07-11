using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class N_PowerUpScript : NetworkBehaviour {

    public float jumpPower = 20f;
    //[SerializeField]
    public GameObject ground;
    [SyncVar]
    bool started = false;

    //[SyncVar]
    //private Vector3 syncPos;

    private void Awake()
    {
        //ground = GameObject.FindGameObjectWithTag("ground");
        //syncPos = GetComponent<Transform>().position;
    }

    /*
    // Use this for initialization
    void Start ()
    {
        float newX = Random.Range(-10, 10);
        float newY = Random.Range(ground.transform.position.y + 100, ground.transform.position.y + 800);
        float newZ = Random.Range(-10, 10);

        transform.position = new Vector3(newX, newY, newZ);
        //RpcSendPos();
    }*/

    private void OnEnable ()
    {
        setNewPosition();
        //if (isServer) RpcSendPos();
    }

    public void setNewPosition()
    {
        if (started)
        {
            float newX = Random.Range(-45f, 45f);
            float newY = Random.Range(transform.position.y + 700, transform.position.y + 1300);
            float newZ = Random.Range(-45f, 45f);

            transform.position = new Vector3(newX, newY, newZ);
        }
        else
        {
            float newX = Random.Range(-45f, 45f);
            float newY = Random.Range(ground.transform.position.y + 100, ground.transform.position.y + 800);
            float newZ = Random.Range(-45f, 45f);

            transform.position = new Vector3(newX, newY, newZ);
            started = true;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Platform")
        {
            float newX = Random.Range(-45f, 45f);
            float newY = Random.Range(transform.position.y - 10, transform.position.y + 10);
            float newZ = Random.Range(-45f, 45f);

            transform.position = new Vector3(newX, newY, newZ);
        }
        if(col.gameObject.tag == "player")
        {
            col.gameObject.GetComponent<N_PlayerMovement>().powerJump(jumpPower);
            setNewPosition();
        }
    }
    /*
    [ClientRpc]
    void RpcSendPos()
    {
        syncPos = transform.position;
    }*/
}
