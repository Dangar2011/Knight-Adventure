using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject victoryPanel;
    private bool isOpenVictory = false;
    public Slider musicSlider,sfxSlider;
    public Toggle fullScreenToggle;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume",1);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {          
             if(PlayerPrefs.GetInt("isFullScreen") == 1)
            {
                fullScreenToggle.isOn = true;
                Screen.fullScreen = true;
            }
            else
            {
                fullScreenToggle.isOn = false;
                Screen.fullScreen = false;
            }

            if (!Screen.fullScreen)
            {
                int screenWidth = PlayerPrefs.GetInt("screenWidth", 1280);
                int screenHeight = PlayerPrefs.GetInt("screenHeight", 720);
                Screen.SetResolution(screenWidth, screenHeight, false);
            }
        }

    }

    private void Update()
    {
        if (FinishPoint.isDone && !isOpenVictory)
        {
            isOpenVictory = true;
            Victory();

        }
    }
    public void ApplicationQuit()
    {
        Application.Quit();
    }
    public void OpenMainMenu()
    {
        Time.timeScale = 1f;
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
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Victory()
    {
       if(victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            Transform text = victoryPanel.transform.Find("Menu");
            TextMeshProUGUI[] textMeshProArray = text.GetComponentsInChildren<TextMeshProUGUI>();
            TextMeshProUGUI scoreText = textMeshProArray[0];
            TextMeshProUGUI coinText = textMeshProArray[1];
            scoreText.text ="Score: " + Score.score.ToString();
            coinText.text = "Coin: " + FinishPoint.coin.ToString();

        }
    }
    
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ButtonClickSound()
    {
        if(Input.GetMouseButtonUp(0))
        AudioManager.Instance.PlaySFX("Click Button");
    }
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.Save();
    }
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVolume",sfxSlider.value );
        PlayerPrefs.Save();
    }
    public void ToggleFullScreen(bool isFullScreen)
    {

        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("isFullScreen", isFullScreen ? 1 : 0);
        if (!isFullScreen)
        {
            int screenWidth = PlayerPrefs.GetInt("screenWidth", 1280);
            int screenHeight = PlayerPrefs.GetInt("screenHeight", 720);
            Screen.SetResolution(screenWidth, screenHeight, false);
        }
    }
}
