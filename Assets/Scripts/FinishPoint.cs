using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{

    public static float coin = 0;
    public TextMeshProUGUI coinText;
    public Transform enemy; 
    private Animator anim;
    public static bool isFinish = false;
    private bool isOpenGate;
    public static bool isDone = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        coinText.text = coin.ToString();
        Debug.Log(EnemyCount.enemyCount);
        if(!isOpenGate )
        {
            if(EnemyCount.enemyCount == 0)
            {
            isOpenGate = true;
            OpenGate();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            Debug.Log((SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("UnlockedLevel")));
        if(EnemyCount.enemyCount == 0)
        {
            isDone = true;
            float currentCoin = PlayerPrefs.GetFloat("PlayerCoin");
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
