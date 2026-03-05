using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject TapToStart;
    public GameObject scoreText;
    
    private void Start()
    {
        gameOverPanel.SetActive(false);
        PauseGame();
        TapToStart.SetActive(true);
        scoreText.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartGame();
            

        }
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        scoreText.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void StartGame()
    {
        Time.timeScale = 1f;
        TapToStart.SetActive(false);
        scoreText.SetActive(true);
    }
}
