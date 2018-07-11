using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class N_setPlatformScript : NetworkBehaviour {

    GameObject[] setPlatforms;

    private void OnEnable()
    {
        setNewPosition();
    }

    public void setNewPosition()
    {
        float maxHeight = 0f;
        if (this.gameObject.tag == "SetPlatforms")
        {
            setPlatforms = GameObject.FindGameObjectsWithTag("SetPlatforms");
            for(int i = 0; i < setPlatforms.Length; i++)
            {
                if(maxHeight < setPlatforms[i].transform.position.y)
                {
                    maxHeight = setPlatforms[i].transform.position.y;
                }
            }
            float newX = Random.Range(-15f, 15f);
            float newY = maxHeight + 10;
            float newZ = Random.Range(-15f, 15f);
            transform.position = new Vector3(newX, newY, newZ);
        }
        else if (this.gameObject.tag == "SetPlatforms2")
        {
            setPlatforms = GameObject.FindGameObjectsWithTag("SetPlatforms2");
            for (int i = 0; i < setPlatforms.Length; i++)
            {
                if (maxHeight < setPlatforms[i].transform.position.y)
                {
                    maxHeight = setPlatforms[i].transform.position.y;
                }
            }
            float newX = Random.Range(-45f, 45f);
            float newY = maxHeight + 10;
            float newZ = Random.Range(-45f, 45f);
            transform.position = new Vector3(newX, newY, newZ);
        }
    }
}
