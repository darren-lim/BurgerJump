using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class N_SpectateCam : NetworkBehaviour {

    //when this script is enabled, disable components, camera locks on to first player in list, or allows for free movement

    private const float Y_Angle_Min = -89f;
    private const float Y_Angle_Max = 89f;

    [Header("SETUP FIELDS")]
    [SerializeField]
    Behaviour[] DisableForSpectate;
    [SerializeField]
    List<Transform> PlayersInGame;

    //public Transform camTransform;
    public float sensHorizontal;
    public float sensVertical;

    public bool isThirdPerson;

    [Header("OnPersonSpectate")]
    private float distance = 10f;
    private float currentX = 0f;
    private float currentY = 0f;

    private int playerIndex;

    [Header("OnFreeSpectate")]
    float camSpeed;

    private void Start()
    {
        //camTransform = transform;
        playerIndex = 0;
        isThirdPerson = true;
    }

    private void OnEnable()
    {
        sensHorizontal = PlayerPrefs.GetFloat("localSens", 6);
        sensVertical = PlayerPrefs.GetFloat("localSens", 6);
        if (isLocalPlayer)
        {
            for (int i = 0; i < DisableForSpectate.Length; i++)
            {
                DisableForSpectate[i].enabled = false;
            }
            getPlayers();
            if(PlayersInGame.Count > 0)
            {
                transform.position = PlayersInGame[0].position;
            }
            else
            {
                isThirdPerson = false;
                //move to free spectate
            }
        }
    }


    void Update ()
    {
        if (!isLocalPlayer)
            return;
        if(PlayersInGame[playerIndex] == null && isThirdPerson)
        {
            getPlayers();
            RotateThroughPlayers();
        }
        if (isThirdPerson)
        {
            currentX += Input.GetAxis("Mouse X") * sensHorizontal;
            currentY += Input.GetAxis("Mouse Y") * sensVertical;

            currentY = Mathf.Clamp(currentY, Y_Angle_Min, Y_Angle_Max);
        }
        if (Input.GetKeyDown(KeyCode.R) || (Input.GetKeyDown(KeyCode.Space) && !isThirdPerson))
        {
            isThirdPerson = true;
            RotateThroughPlayers();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isThirdPerson)
        {
            isThirdPerson = false;
            //Free spectate
        }
	}

    private void LateUpdate()
    {
        if (isThirdPerson)
        {
            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            transform.position = PlayersInGame[playerIndex].position + rotation * dir;
            transform.LookAt(PlayersInGame[playerIndex].position);
        }
    }

    //OnPerson
    public void getPlayers()
    {
        PlayersInGame.Clear();
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("player");
        foreach (GameObject player in playerList)
        {
            if(player.GetComponent<MeshRenderer>().enabled)
                PlayersInGame.Add(player.transform);
        }
    }
    //OnPerson
    void RotateThroughPlayers()
    {
        if (playerIndex < PlayersInGame.Count)
            playerIndex++;
        else
            playerIndex = 0;

    }

    //FreeSpectate
}
