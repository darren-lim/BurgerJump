using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;

public class N_GroundScript : NetworkBehaviour {

    [SyncVar]
    public float speed = 5f;

    private BoxCollider box;

    public Material material;
    private Renderer rend;

    private void Awake()
    {
        this.enabled = false;
        box = GetComponent<BoxCollider>();
        rend = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        rend.sharedMaterial = material;
        box.isTrigger = true;
    }

    // Update is called once per frame
    void Update ()
    {
        transform.position = new Vector3(0, transform.position.y + (Time.deltaTime * speed), 0);
	}

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Platform") //|| collider.gameObject.tag == "Enemy")
        {
            collider.gameObject.GetComponent<N_PlatformScript>().setNewPosition();
        }
        else if (collider.gameObject.tag == "SetPlatforms" || collider.gameObject.tag == "SetPlatforms2")
        {
            collider.gameObject.GetComponent<N_setPlatformScript>().setNewPosition();
        }
        else if (collider.gameObject.tag == "PowerUp")
        {
            collider.gameObject.GetComponent<N_PowerUpScript>().setNewPosition();
        }
        
    }

    public void addSpeed(float s)
    {
        speed += s;
    }

    public float getSpeed()
    {
        return speed;
    }

    public void subtractSpeed(float s)
    {
        speed -= s;
    }
}
