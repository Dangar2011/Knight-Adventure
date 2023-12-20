using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{

    public static float coin = 0;
    public TextMeshProUGUI coinText;
   // public Transform enemy; 
    private Animator anim;
    public static bool isFinish = false;
    private bool isOpenGate;
    public static bool isDone = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        coin = 0;
        isDone = false;
        isFinish = false;
    }

    void Update()
    {
        coinText.text = coin.ToString();
        if(!isOpenGate )
        {
            if(EnemyCount.enemyCount == 0 )
            {
            isOpenGate = true;
            OpenGate();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(("buildIndex: "+SceneManager.GetActiveScene().buildIndex));
        Debug.Log(("UnlockLevel: "+PlayerPrefs.GetInt("UnlockedLevel")));
        Debug.Log((SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("UnlockedLevel")));
        if(EnemyCount.enemyCount == 0)
        {
            isDone = true;
            AudioManager.Instance.musicSource.Stop();
            AudioManager.Instance.PlaySFX("Victory");
            float currentCoin = PlayerPrefs.GetFloat("PlayerCoin");
            float level = SceneManager.GetActiveScene().buildIndex;
            float highScore = PlayerPrefs.GetFloat("HighScore" + level);
            if(Score.score > highScore)
            {
                PlayerPrefs.SetFloat("HighScore" + level, Score.score);
            }
            PlayerPrefs.SetFloat("PlayerCoin", currentCoin + coin);
            PlayerPrefs.Save();
            CompleteLevel();
        }
    }
    public void OpenGate()
    {
        anim.SetTrigger("isFinish");
    }
    private void CompleteLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("UnlockedLevel"))
        {          
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
   
}
