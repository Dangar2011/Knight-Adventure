using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float playerHP = 100f;
    private float playerMP = 100f;
    private int playerDamage = 10;

    private void Start()
    {
        LoadPlayerData();
    }

    public void SetPlayerHP(float value = 10f)
    {
        playerHP += value;
        SavePlayerData();
    }

    public float GetPlayerHP()
    {
        return playerHP;
    }

    public void SetPlayerMP(float value = 10f)
    {
        playerMP += value;
        SavePlayerData();
    }

    public float GetPlayerMP()
    {
        return playerMP;
    }

    public void SetPlayerDamage(int value = 2)
    {
        playerDamage += value;
        SavePlayerData(); 
    }

    public int GetPlayerDamage()
    {
        return playerDamage;
    }

    private void SavePlayerData()
    {
        PlayerPrefs.SetFloat("PlayerHP", playerHP);
        PlayerPrefs.SetFloat("PlayerMP", playerMP);
        PlayerPrefs.SetInt("PlayerDamage", playerDamage);
    }

    private void LoadPlayerData()
    {
        playerHP = PlayerPrefs.GetFloat("PlayerHP", 100f);
        playerMP = PlayerPrefs.GetFloat("PlayerMP", 100f);
        playerDamage = PlayerPrefs.GetInt("PlayerDamage", 10);
    }
}
