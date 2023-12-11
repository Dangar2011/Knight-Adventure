using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    [SerializeField] private Image currentHP;
    [SerializeField] private Image currentSpeed;
    [SerializeField] private Image currentDamage;
    private float initialHP = 100f;
    private float initialSpeed = 5f;
    private float initialDamage = 10f;
    private float currentCoin = 0;
    private float coinToUpHP = 10f;
    private float coinToUpSpeed = 10f;
    private float coinToUpDamage = 10f;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI coinToUpHPText;
    public TextMeshProUGUI coinToUpSpeedText;
    public TextMeshProUGUI coinToUpDamageText;
    public Button upgradeHP;
    public Button upgradeSpeed;
    public Button upgradeDamage;
    void Start()
    { 
        GetCoinToUp();
        player = GetComponent<Player>();    

        coinToUpHPText.text = coinToUpHP.ToString();
        coinToUpSpeedText.text = coinToUpSpeed.ToString();
        coinToUpDamageText.text = coinToUpDamage.ToString();

        currentHP.fillAmount = (player.GetPlayerHP() - initialHP) / (player.maxPlayerHP - initialHP);
        currentSpeed.fillAmount = (player.GetPlayerSpeed() - initialSpeed) / (player.maxPlayerSpeed - initialSpeed);
        currentDamage.fillAmount = (player.GetPlayerDamage() - initialDamage) / (player.maxPlayerDamage - initialDamage);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetPlayerCoin() < coinToUpHP) upgradeHP.interactable = false;
        else if (player.GetPlayerHP() == player.maxPlayerHP)
        {
            upgradeHP.interactable = false;
            coinToUpHPText.text = "MAX";
        }
        else upgradeHP.interactable = true;
        if (player.GetPlayerCoin() < coinToUpSpeed ) upgradeSpeed.interactable = false;
        else if (player.GetPlayerSpeed() == player.maxPlayerSpeed)
        {
            upgradeSpeed.interactable = false;
            coinToUpSpeedText.text = "MAX";
        }
        else upgradeSpeed.interactable = true;
        if (player.GetPlayerCoin() < coinToUpDamage) upgradeDamage.interactable = false;
        else if (player.GetPlayerDamage() == player.maxPlayerDamage)
        {
            upgradeDamage.interactable = false;
            coinToUpDamageText.text = "MAX";
        }
        else upgradeDamage.interactable = true;
        currentCoin = player.GetPlayerCoin();
        coinText.text = currentCoin.ToString();
        
    }

    public void Cheat()
    {
        player.SetPlayerCoin(100);
    }
    public void DeleteAllKey()
    {
        PlayerPrefs.DeleteKey("PlayerHP");
        PlayerPrefs.DeleteKey("PlayerDamage");
        PlayerPrefs.DeleteKey("PlayerSpeed");
        PlayerPrefs.DeleteKey("coinHP");
        PlayerPrefs.DeleteKey("coinDamage");
        PlayerPrefs.DeleteKey("coinSpeed");

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    public void UpgradeHP()
    {
        if(player.GetPlayerCoin() >= coinToUpHP)
        {
            player.SetPlayerCoin(-coinToUpHP);
            coinToUpHP = Mathf.Clamp(coinToUpHP + 5, 0, 30);
            coinToUpHPText.text = coinToUpHP.ToString();
            player.SetPlayerHP();
            currentHP.fillAmount = ((player.GetPlayerHP() - initialHP) / (player.maxPlayerHP - initialHP));
            SetCoinToUp();
        }
    }
    public void UpgradeSpeed()
    {
        if (player.GetPlayerCoin() >= coinToUpSpeed)
        {
            player.SetPlayerCoin(-coinToUpSpeed);
            coinToUpSpeed = Mathf.Clamp(coinToUpSpeed + 5, 0,30);
            coinToUpSpeedText.text = coinToUpSpeed.ToString();
            player.SetPlayerSpeed();
            currentSpeed.fillAmount = (player.GetPlayerSpeed() - initialSpeed) / (player.maxPlayerSpeed - initialSpeed);
            SetCoinToUp();
        }
    }
    public void UpgradeDamage()
    {
        if (player.GetPlayerCoin() >= coinToUpDamage)
        {
            player.SetPlayerCoin(-coinToUpDamage);
            coinToUpDamage = Mathf.Clamp(coinToUpDamage + 5, 0, 30);
            coinToUpDamageText.text = coinToUpDamage.ToString();
            player.SetPlayerDamage();
            currentDamage.fillAmount = (player.GetPlayerDamage() - initialDamage) / (player.maxPlayerDamage - initialDamage);
            SetCoinToUp();
        }
    }
    public void SetCoinToUp()
    {
        PlayerPrefs.SetFloat("coinHP",coinToUpHP);
        PlayerPrefs.SetFloat("coinSpeed", coinToUpSpeed);
        PlayerPrefs.SetFloat("coinDamage", coinToUpDamage);
        PlayerPrefs.Save();
    }
    public void GetCoinToUp()
    {
        coinToUpHP = PlayerPrefs.GetFloat("coinHP",10f);
        coinToUpSpeed = PlayerPrefs.GetFloat("coinSpeed",10f);
        coinToUpDamage = PlayerPrefs.GetFloat("coinDamage",10f);
    }
}

