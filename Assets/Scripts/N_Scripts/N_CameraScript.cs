using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class N_CameraScript : NetworkBehaviour {

    //moving camera on x and y axis
	public enum RotationAxis
    {
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxis axes1 = RotationAxis.MouseX;
    public RotationAxis axes2 = RotationAxis.MouseY;
    public float minVert = -90f;
    public float maxVert = 90f;
    //sensitivity
    public float sensHorizontal;
    public float sensVertical;

    public float rotationX = 0;

    public bool paused;

    bool ischild = false;

    [SerializeField]
    Camera cam;

    private void Start()
    {
        paused = false;
        //hides cursor and locks it to the center
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        sensHorizontal = PlayerPrefs.GetFloat("localSens", 6);
        sensVertical = PlayerPrefs.GetFloat("localSens", 6);
    }

    // Update is called once per frame
    void Update ()
    {
        if (N_PauseMenu.isOn) return;
        //get sensitivity and allows input to move camera
        if (isLocalPlayer || ischild)
        {
            if (sensHorizontal != PlayerPrefs.GetFloat("localSens"))
            {
                sensHorizontal = PlayerPrefs.GetFloat("localSens",6);
                sensVertical = PlayerPrefs.GetFloat("localSens",6);
            }
            if (axes1 == RotationAxis.MouseX)
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensHorizontal, 0);
            if (axes2 == RotationAxis.MouseY && cam != null)
            {
                rotationX -= Input.GetAxis("Mouse Y") * sensVertical;
                rotationX = Mathf.Clamp(rotationX, minVert, maxVert); //clamps vertical angle within max and min limits

                //float rotationY = transform.localEulerAngles.y;
                cam.transform.localEulerAngles = new Vector3(rotationX, 0, 0);
            }
        }
	}
}
