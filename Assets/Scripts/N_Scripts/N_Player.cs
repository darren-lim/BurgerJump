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
        username = PlayerPrefs.GetString("username", "Player");
        usernameText.text = PlayerPrefs.GetString("username", "Player");
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
                        /*
                        if (isServer)
                            RpcReady(true);
                        else
                            CmdReady(true);*/
                        readyText.enabled = true;
                        //Add a ui text
                        //restart score UI or disable it
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
                        /*
                        if (isServer)
                            RpcReady(false);
                        else
                            CmdReady(false);*/
                        readyText.enabled = false;
                        //Add a ui text
                        //restart score UI or disable it
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
    }

    public void setSpectatingTrue()
    {
        isSpectating = true;
        spectatingText.enabled = true;
        finalScore.enabled = true;
        finalScore.text = "Final " + ScoreText.text;
        ScoreText.enabled = false;
        usernameText.enabled = false;
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
    /*
    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }


    
    [SerializeField] ToggleEvent onToggleShared;
    [SerializeField] ToggleEvent onToggleLocal;
    [SerializeField] ToggleEvent onToggleRemote;

    public GameObject mainCamera;

    private void Start()
    {
        mainCamera = Camera.main.gameObject;

        EnablePlayer();
    }

    void DisablePlayer()
    {
        if (isLocalPlayer)
        {
            mainCamera.SetActive(true);
        }

        onToggleShared.Invoke(false);

        if (isLocalPlayer)
            onToggleLocal.Invoke(false);
        else
            onToggleRemote.Invoke(false);
    }

    void EnablePlayer()
    {
        if (isLocalPlayer)
        {
            mainCamera.SetActive(false);// turns off main camera in scene
        }

        onToggleShared.Invoke(true); //turn on anyhting shared

        if (isLocalPlayer)
            onToggleLocal.Invoke(true);
        else
            onToggleRemote.Invoke(true);
    }
    */
}
