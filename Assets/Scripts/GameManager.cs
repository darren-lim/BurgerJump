using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private Transform player;
    public float maxHeightAchieved = 0f;
    public float currheight = 0f;

    public Text maxHeightText;
    public Text currentHeightText;

    public GameObject ground;
    private GroundScript groundScript;
    public GameObject[] poolers;

    private bool startClimb = false;
    public float heightAchievs;
    //public bool instantiatePlatforms = false;

	// Use this for initialization
	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        heightAchievs = 100f;
        groundScript = ground.GetComponent<GroundScript>();
    }


    // Update is called once per frame
    void Update ()
    {
        if (startClimb == false && player.position.y > 25)
        {
            player.GetComponent<PlayerMovement>().jumpForce = 30;
            startClimb = true;
        }

        if (player.position.y > maxHeightAchieved)
        {
            maxHeightAchieved = player.position.y;
        }
        maxHeightAchieved = Mathf.Round(maxHeightAchieved * 100f) / 100f;
        currheight = Mathf.Round(player.position.y * 100f) / 100f;
        maxHeightText.text = "Max Height Reached: " + maxHeightAchieved.ToString();
        currentHeightText.text = "Current Height: " + currheight.ToString();

        if (maxHeightAchieved > 25f)
        {
            groundScript.enabled = true;
            //instantiatePlatforms = true;

            //first index is platform pooler
            GameObject platform = poolers[0].GetComponent<ObjectPoolerScript>().GetPooledObject();
            if (platform != null)
            {
                platform.SetActive(true);
            }
            
        }
        if((player.position.y - ground.transform.position.y) > 150)
        {
            poolers[0].GetComponent<ObjectPoolerScript>().willGrow = true;
        }
        else
        {
            poolers[0].GetComponent<ObjectPoolerScript>().willGrow = false;
        }

        if(maxHeightAchieved > heightAchievs && groundScript.speed < 8)
        {
            ground.GetComponent<GroundScript>().addSpeed(1);
            heightAchievs += 150f;
        }

        if(player.position.y < ground.transform.position.y)
        {
            this.GetComponent<SceneManagerScript>().GameOver();
        }
        /*
        if (instantiatePlatforms)
        {
            //first index is platform pooler
            GameObject platform = poolers[0].GetComponent<ObjectPoolerScript>().GetPooledObject();
            if (platform != null)
            {
                platform.SetActive(true);
            }

        }*/
    }
}
