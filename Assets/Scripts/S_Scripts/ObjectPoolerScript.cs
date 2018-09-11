using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerScript : MonoBehaviour {

    public static ObjectPoolerScript current;
    public GameObject pooledObject;
    public int pooledAmount = 20;

    List<GameObject> pooledObjects;

    private void Awake()
    {
        current = this;
    }

    void Start ()
    {
        //Pool the object referenced in the Inspector to pooled amount.
        //then, add the object to the list to reference it.
        pooledObjects = new List<GameObject>();
        for(int i = 0; i < pooledAmount; ++i)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(true);
            pooledObjects.Add(obj);
        }
	}
	
    //gets an object from the list pooledObjects.
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; ++i)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
