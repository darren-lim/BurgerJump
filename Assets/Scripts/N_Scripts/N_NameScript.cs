using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_NameScript : MonoBehaviour {

    [SerializeField]
    string playerName = "Player";

    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }
    public void ChangeName(string name)
    {
        playerName = name;
    }

    public string getName()
    {
        return playerName;
    }
}
