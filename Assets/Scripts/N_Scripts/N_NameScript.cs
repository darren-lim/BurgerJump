using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_NameScript : MonoBehaviour {

    public void ChangeName(string name)
    {
        PlayerPrefs.SetString("username", name);
    }
}
