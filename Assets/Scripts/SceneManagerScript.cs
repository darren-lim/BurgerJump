using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript: MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject gameOverCanvas;
    //if multiplayer, change this concept
    public GameObject player;
    public Text gameOverScore;

    public bool isPaused = false;
    public bool isGameOver = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            PauseOrResume();
        }
    }

    public void loadlevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PauseOrResume()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            isPaused = true;
            //player.GetComponent<CameraScript>().enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            isPaused = false;
            //player.GetComponent<CameraScript>().enabled = true;
        }
        pauseCanvas.SetActive(isPaused);
        cameraPause();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        isPaused = true;
        cameraPause();
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        float score = gameObject.GetComponent<GameManager>().maxHeightAchieved;
        score = Mathf.Round(score * 100f) / 100f;
        gameOverScore.text = score.ToString();
    }

    private void cameraPause()
    {
        CameraScript[] scripts = player.GetComponentsInChildren<CameraScript>();
        foreach (CameraScript cam in scripts)
        {
            cam.paused = isPaused;
        }
    }
}

