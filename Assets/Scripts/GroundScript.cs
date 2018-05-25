using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour {

    public float speed = 1f;
    public GameObject gamemanager;

    private BoxCollider box;

    private void Awake()
    {
        this.enabled = false;
    }

    private void OnEnable()
    {
        box = GetComponent<BoxCollider>();

        box.isTrigger = true;
    }

    // Update is called once per frame
    void Update ()
    {
        transform.position = new Vector3(0, transform.position.y + (Time.deltaTime * speed), 0);
	}

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            //animation maybe?
            gamemanager.GetComponent<SceneManagerScript>().GameOver();
        }
        if(collider.gameObject.tag == "Platform")
        {
            Debug.Log("COLLIDE");
            collider.gameObject.SetActive(false);
        }
    }

    public void addSpeed(float s)
    {
        speed += s;
    }
}
