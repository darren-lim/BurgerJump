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
    private Transform ground;

    [Header("UI")]
    public float Score = 0f;
    public Text ScoreText;
    public Text fpsText;
    public Text spectatingText;
    public Text PowerUpText;
    public Text finalScore;
    private float deltaTime = 0f;
    private float maxHeightAchieved = 0f;
    private float powerUpTime;
    private bool isSpectating = false;


    private void Start()
    {
        ground = GameObject.FindGameObjectWithTag("ground").transform;
        if (!isLocalPlayer)
        {
            for(int i = 0; i<componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
        spectatingText.enabled = false;
        PowerUpText.enabled = false;
        finalScore.enabled = false;
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            if (!isSpectating)
            {
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
        }
    }

    public void setSpectatingTrue()
    {
        isSpectating = true;
        spectatingText.enabled = true;
        finalScore.enabled = true;
        finalScore.text = "Final " + ScoreText.text;
        ScoreText.enabled = false;
    }

    public void setPowerUpTime(float time)
    {
        powerUpTime = time;
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
