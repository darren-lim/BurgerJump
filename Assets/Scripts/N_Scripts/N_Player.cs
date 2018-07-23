using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
//using UnityEngine.Events;

//[System.Serializable]
//public class ToggleEvent : UnityEvent<bool> { }

public class N_Player : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;
    [SerializeField]
    Camera sceneCamera;
    [SerializeField]
    private Transform ground;
    [SerializeField]
    private N_GameManagerScript gameManager;

    public bool isReady = false;

    [SyncVar]
    public string username = "Player";

    [Header("UI")]
    public float Score = 0f;
    public Text ScoreText;
    public Text fpsText;
    public Text spectatingText;
    public Text PowerUpText;
    public Text finalScore;
    public Text readyText;
    public Text usernameText;
    public Text rPlayersText;
    private float deltaTime = 0f;
    private float maxHeightAchieved = 0f;
    private float powerUpTime;
    private bool isSpectating = false;

    [SerializeField]
    GameObject pauseMenu;


    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<N_GameManagerScript>();
        if (!isLocalPlayer)
        {
            for(int i = 0; i<componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
            usernameText.enabled = true;
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
            usernameText.enabled = false;
        }
        spectatingText.enabled = false;
        PowerUpText.enabled = false;
        finalScore.enabled = false;
        readyText.enabled = false;
        N_PauseMenu.isOn = false;
        ScoreText.enabled = false;
        rPlayersText.enabled = false;

        username = PlayerPrefs.GetString("username", "Player");
        CmdSendName(username);
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            if (!isSpectating)
            {
                if(!isReady && !gameManager.gameStart)
                {
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        isReady = true;
                        if (isClient) CmdReady(true);
                        else gameManager.numPlayersReady++;
                        readyText.enabled = true;
                    }
                }
                else if (isReady && !gameManager.gameStart)
                {
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        if (gameManager.countDownStart) return;
                        isReady = false;
                        if (isClient) CmdReady(false);
                        else gameManager.numPlayersReady++;
                        readyText.enabled = false;
                    }
                }
                else if(isReady && gameManager.gameStart)
                {
                    isReady = false;
                    readyText.enabled = false;
                    ScoreText.enabled = true;
                    //clear ui cause game started
                }
                //UI TEXTS
                if (transform.position.y > maxHeightAchieved)
                {
                    maxHeightAchieved = transform.position.y;
                }
                Score = Mathf.Round(maxHeightAchieved * 100f) / 100f;
                ScoreText.text = "Score: " + Mathf.Round(maxHeightAchieved * 6).ToString();

                if (powerUpTime > 0)
                {
                    PowerUpText.enabled = true;
                    powerUpTime -= Time.deltaTime;
                    float seconds = powerUpTime % 60;
                    PowerUpText.text = "Power Jump! \n" + Mathf.RoundToInt(seconds).ToString() + " s";
                }
                else
                    PowerUpText.enabled = false;
            }
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePauseMenu();
            }
        }

        RpcSendName(username);
    }

    public void setSpectatingTrue()
    {
        isSpectating = true;
        spectatingText.enabled = true;
        finalScore.enabled = true;
        finalScore.text = "Final " + ScoreText.text;
        ScoreText.enabled = false;
        usernameText.enabled = false;
        rPlayersText.enabled = true;
    }

    public void setPowerUpTime(float time)
    {
        powerUpTime = time;
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        N_PauseMenu.isOn = pauseMenu.activeSelf;

        if (Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void changeSens(float num)
    {
        PlayerPrefs.SetFloat("localSens", num);
    }

    [Command]
    void CmdReady(bool ready)
    {
        //RpcReady(ready);
        if(ready)
            gameManager.numPlayersReady++;
        else
            gameManager.numPlayersReady--;
    }

    [Command]
    void CmdSendName(string name)
    {
        username = name;
        RpcSendName(name);
    }

    [ClientRpc]
    void RpcSendName(string name)
    {
        usernameText.text = name;
    }
}
