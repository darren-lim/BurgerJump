using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    private const float Y_Angle_Min = -89f;
    private const float Y_Angle_Max = 89f;
    private float distance = 7f;

    private float currentX = 0f;
    private float currentY = 0f;

    private float sensHorizontal;
    private float sensVertical;

    public Transform camPos;

    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        sensHorizontal = PlayerPrefs.GetFloat("localSens", 6);
        sensVertical = PlayerPrefs.GetFloat("localSens", 6);
    }

    private void OnDisable()
    {
        transform.position = camPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
            return;
        if (sensHorizontal != PlayerPrefs.GetFloat("localSens", 6))
        {
            sensHorizontal = PlayerPrefs.GetFloat("localSens", 6);
            sensVertical = PlayerPrefs.GetFloat("localSens", 6);
        }

        currentX += Input.GetAxis("Mouse X") * sensHorizontal;
        currentY -= Input.GetAxis("Mouse Y") * sensVertical;

        currentY = Mathf.Clamp(currentY, Y_Angle_Min, Y_Angle_Max);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        if (paused)
            return;
        ThirdPersonPos();
        //this.transform.localRotation = Quaternion.identity;
    }

    void ThirdPersonPos()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = transform.parent.position + rotation * dir;
        transform.LookAt(transform.parent.position);
        transform.parent.rotation = Quaternion.Euler(0, currentX, 0);
    }
}
