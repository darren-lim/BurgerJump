using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class N_GameManagerScript : NetworkBehaviour {

    public Text groundText;
    public Text gameStartText;
    public Text waitingForPlayersText;
    public Text pressRtoReadyText;
    public Text winnerText;
    public Text restartGameText;
    [SyncVar]
    private float groundTime;
    [SyncVar]
    private float gameStartTime;
    [SyncVar]
    private float gameEndTime;
    [SyncVar]
    private float groundHeightThresh;

    public NetworkManager nManager;

    private N_GroundScript groundScript;
    public GameObject[] poolers;
    [SerializeField]
    private N_ObjectPoolerScript[] platforms;
    [SyncVar]
    public bool gameStart = false;
    [SyncVar]
    public bool countDownStart = false;
    [SyncVar]
    public int reqNumPlayers = 2;
    [SyncVar]
    public int numPlayersReady;

    private void Start()
    {
        numPlayersReady = 0;
        gameStartTime = 5f;
        gameEndTime = 5f;
        groundTime = 20f;
        groundHeightThresh = 300f;
        groundText.enabled = false;
        gameStartText.enabled = false;
        pressRtoReadyText.enabled = false;
        winnerText.enabled = false;
        restartGameText.enabled = false;
        nManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>();
        groundScript = GameObject.FindGameObjectWithTag("ground").GetComponent<N_GroundScript>();
    }

    public override void OnStartServer()
    {
        if (isServer)
        {
            platforms = new N_ObjectPoolerScript[poolers.Length];
            for (int i = 0; i < poolers.Length; ++i)
            {
                platforms[i] = poolers[i].GetComponent<N_ObjectPoolerScript>();
            }
            RpcInitialTextSetup();
        }
        base.OnStartServer();
    }

    private void Update()
    {
        if (groundTime > 0f && gameStart)
        {
            groundText.enabled = true;
            StartCoroutine("TimeGround");
        }
        if (!gameStart)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("player");
            if (players.Length >= reqNumPlayers)
            {
                waitingForPlayersText.enabled = false;
                pressRtoReadyText.enabled = true;
                if (players.Length == numPlayersReady)
                {
                    pressRtoReadyText.enabled = false;
                    gameStartText.enabled = true;
                    StartCoroutine("GameStartTimer");
                }
            }
        }
        else
        {/*
            if (isServer)
            {
                for (int i = 0; i < platforms.Length; ++i)
                {
                    GameObject platform = platforms[i].GetPooledObject();
                    if (platform != null)
                    {
                        platform.SetActive(true);
                    }
                }
            }*/
            if (groundScript.transform.position.y > groundHeightThresh && groundScript.speed < 10)
            {
                groundScript.addSpeed(1);
                groundHeightThresh += 300;
            }
            GameObject[] players = GameObject.FindGameObjectsWithTag("player");
            if (players.Length == 1)
            {
                //show whos winner
                //string winner = GameObject.FindGameObjectWithTag("player").GetComponent<N_Player>().getName();
                winnerText.enabled = true;
                winnerText.text = players[0].GetComponent<N_Player>().username + " Wins!";
                //end game in a few seconds, restart scene
                StartCoroutine("EndGame");
            }
        }
    }

    IEnumerator TimeGround()
    {
        while (groundTime > 0f)
        {
            groundTime -= Time.deltaTime;
            float seconds = groundTime % 60;
            groundText.text = "Ground Rising In " + Mathf.RoundToInt(seconds).ToString() + " s";
            yield return new WaitForSeconds(20f);
        }
        if (groundTime <= 0)
        {
            if (isServer)
            {
                RpcStartGround();
                groundScript.enabled = true;
            }
            //CmdStartGround();
            groundText.enabled = false;
            yield break;
        }
    }

    IEnumerator GameStartTimer()
    {
        countDownStart = true;
        gameStartTime -= Time.deltaTime;
        float seconds = gameStartTime % 60;
        gameStartText.text = "Game Starting In " + Mathf.RoundToInt(seconds).ToString() + " s";
        yield return new WaitForSeconds(5f);
        gameStartText.enabled = false;
        if(isServer) activateAllPoolers();
        gameStart = true;

        gameStartTime = 6f;

        yield break;
    }

    IEnumerator EndGame()
    {
        restartGameText.enabled = true;
        gameEndTime -= Time.deltaTime;
        float seconds = gameEndTime % 60;
        restartGameText.text = "Game Ending In " + Mathf.RoundToInt(seconds).ToString() + " s";
        yield return new WaitForSeconds(5f);
        nManager.ServerChangeScene("NetworkScene");

        yield break;
    }

    void activateAllPoolers()
    {
        for (int i = 0; i < poolers.Length; i++)
        {
            poolers[i].SetActive(true);
        }
    }

    [Command]
    void CmdInitialTextSetup()
    {
        RpcInitialTextSetup();
    }
    [ClientRpc]
    void RpcInitialTextSetup()
    {
        groundText.enabled = false;
        waitingForPlayersText.enabled = true;
        gameStartText.enabled = false;
        pressRtoReadyText.enabled = false;
        Debug.Log("True");
    }
    
    [Command]
    void CmdStartGround()
    {
        RpcStartGround();
    }

    [ClientRpc]
    void RpcStartGround()
    {
        groundScript.enabled = true;
    }
}
