using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float playerHP = 100f;
    private float playerMP = 100f;
    private int playerDamage = 10;
     
    public void SetPlayerHP(int value)
    {
        playerHP += value;
        
    }
    public float GetPlayerHP()
    {
        return playerHP;
    }
    public void SetPlayerMP(int value)
    {
        playerHP += value;
    }
    public float GetPlayerMP()
    {
        return playerMP;
    }
    public void SetPlayerDamage(int value)
    {
        playerDamage += value;
    }
    public int GetPlayerDamage()
    {
        return playerDamage;
    }
}
