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
    public GameObject[] poolers;

    private bool startClimb = false;
    //public bool instantiatePlatforms = false;

	// Use this for initialization
	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (player.position.y > maxHeightAchieved)
        {
            maxHeightAchieved = player.position.y;
        }
        maxHeightAchieved = Mathf.Round(maxHeightAchieved * 100f) / 100f;
        currheight = Mathf.Round(player.position.y * 100f) / 100f;
        maxHeightText.text = "Max Height Reached: " + maxHeightAchieved.ToString();
        currentHeightText.text = "Current Height: " + currheight.ToString();

        if (maxHeightAchieved > 15f)
        {
            ground.GetComponent<GroundScript>().enabled = true;
            //instantiatePlatforms = true;

            //first index is platform pooler
            GameObject platform = poolers[0].GetComponent<ObjectPoolerScript>().GetPooledObject();
            if (platform != null)
            {
                platform.SetActive(true);
            }
            
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
