using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    //moving camera on x and y axis
	public enum RotationAxis
    {
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxis axes = RotationAxis.MouseX;
    public float minVert = -50f;
    public float maxVert = 60f;
    //sensitivity
    public float sensHorizontal = 10f;
    public float sensVertical = 10f;

    public float rotationX = 0;

    public bool paused;

    private void Start()
    {
        paused = false;
        //hides cursor and locks it to the center
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update ()
    {
        if (paused)
            return;
        else
        {
            if (axes == RotationAxis.MouseX)
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensHorizontal, 0);
            else if (axes == RotationAxis.MouseY)
            {
                rotationX -= Input.GetAxis("Mouse Y") * sensVertical;
                rotationX = Mathf.Clamp(rotationX, minVert, maxVert); //clamps vertical angle within max and min limits (45 degrees)

                float rotationY = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
            }
        }
	}
}
