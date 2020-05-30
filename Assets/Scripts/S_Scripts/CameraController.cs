using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool thirdPerson = false;
    public CameraScript cameraYScript;
    public CameraScript cameraXScript;
    public ThirdPersonCam thirdPersonScript;

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (thirdPerson)
            {
                cameraXScript.enabled = true;
                cameraYScript.enabled = true;
                thirdPersonScript.enabled = false;
            }
            else
            {
                thirdPersonScript.enabled = true;
                cameraXScript.enabled = false;
                cameraYScript.enabled = false;
            }
            thirdPerson = !thirdPerson;
        }
    }
}
