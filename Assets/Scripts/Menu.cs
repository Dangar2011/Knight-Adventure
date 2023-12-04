using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject victoryPanel;
    private bool isOpenVictory = false;
    public void ApplicationQuit()
    {
        Application.Quit();
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }
    public void RestartGame()
    {
        if (Time.timeScale == 0) Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Victory()
    {
        victoryPanel.SetActive(true);
    }
    private void Update()
    {
        if(FinishPoint.isDone && !isOpenVictory)
        {
            isOpenVictory = true;
            Victory();
        }
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
