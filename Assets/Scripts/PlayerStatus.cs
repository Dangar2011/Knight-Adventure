using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    // Start is called before the first frame update
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
        //GetCoinToUp();
        //player = GetComponent<Player>();    

        //coinToUpHPText.text = coinToUpHP.ToString();
        //coinToUpSpeedText.text = coinToUpSpeed.ToString();
        //coinToUpDamageText.text = coinToUpDamage.ToString();

        //currentHP.fillAmount = (player.GetPlayerHP() - initialHP) / (player.maxPlayerHP - initialHP);
        //currentSpeed.fillAmount = (player.GetPlayerSpeed() - initialSpeed) / (player.maxPlayerSpeed - initialSpeed);
        //currentDamage.fillAmount = (player.GetPlayerDamage() - initialDamage) / (player.maxPlayerDamage - initialDamage);
        GetCoinToUp();
        SetPlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
       
        
    }
    private void UpdateUI()
    {
        if (Player.Instance.GetPlayerCoin() < coinToUpHP) upgradeHP.interactable = false;
        else if (Player.Instance.GetPlayerHP() == Player.maxPlayerHP)
        {
            upgradeHP.interactable = false;
            coinToUpHPText.text = "MAX";
        }
        else upgradeHP.interactable = true;

        if (Player.Instance.GetPlayerCoin() < coinToUpSpeed) upgradeSpeed.interactable = false;
        else if (Player.Instance.GetPlayerSpeed() == Player.maxPlayerSpeed)
        {
            upgradeSpeed.interactable = false;
            coinToUpSpeedText.text = "MAX";
        }
        else upgradeSpeed.interactable = true;

        if (Player.Instance.GetPlayerCoin() < coinToUpDamage) upgradeDamage.interactable = false;
        else if (Player.Instance.GetPlayerDamage() == Player.maxPlayerDamage)
        {
            upgradeDamage.interactable = false;
            coinToUpDamageText.text = "MAX";
        }
        else upgradeDamage.interactable = true;

        currentCoin = Player.Instance.GetPlayerCoin();
        coinText.text = currentCoin.ToString();
    }
    private void SetPlayerStats()
    {
        coinToUpHPText.text = coinToUpHP.ToString();
        coinToUpSpeedText.text = coinToUpSpeed.ToString();
        coinToUpDamageText.text = coinToUpDamage.ToString();

        currentHP.fillAmount = CalculateFillAmount(Player.Instance.GetPlayerHP(), Player.maxPlayerHP, initialHP);
        currentSpeed.fillAmount = CalculateFillAmount(Player.Instance.GetPlayerSpeed(), Player.maxPlayerSpeed, initialSpeed);
        currentDamage.fillAmount = CalculateFillAmount(Player.Instance.GetPlayerDamage(), Player.maxPlayerDamage, initialDamage);
    }
    private float CalculateFillAmount(float currentValue, float maxValue, float initialValue)
    {
        return (currentValue - initialValue) / (maxValue - initialValue);
    }
    public void Cheat()
    {
                    Player.Instance.SetPlayerCoin(100);
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
        if(Player.Instance.GetPlayerCoin() >= coinToUpHP)
        {
            Player.Instance.SetPlayerCoin(-coinToUpHP);
            coinToUpHP = Mathf.Clamp(coinToUpHP + 5, 0, 30);
            coinToUpHPText.text = coinToUpHP.ToString();
            Player.Instance.SetPlayerHP();
            currentHP.fillAmount = ((Player.Instance.GetPlayerHP() - initialHP) / (Player.maxPlayerHP - initialHP));
            SetCoinToUp();
        }
    }
    public void UpgradeSpeed()
    {
        if (Player.Instance.GetPlayerCoin() >= coinToUpSpeed)
        {
            Player.Instance.SetPlayerCoin(-coinToUpSpeed);
            coinToUpSpeed = Mathf.Clamp(coinToUpSpeed + 5, 0,30);
            coinToUpSpeedText.text = coinToUpSpeed.ToString();
            Player.Instance.SetPlayerSpeed();
            currentSpeed.fillAmount = (Player.Instance.GetPlayerSpeed() - initialSpeed) / (Player.maxPlayerSpeed - initialSpeed);
            SetCoinToUp();
        }
    }
    public void UpgradeDamage()
    {
        if (Player.Instance.GetPlayerCoin() >= coinToUpDamage)
        {
            Player.Instance.SetPlayerCoin(-coinToUpDamage);
            coinToUpDamage = Mathf.Clamp(coinToUpDamage + 5, 0, 30);
            coinToUpDamageText.text = coinToUpDamage.ToString();
            Player.Instance.SetPlayerDamage();
            currentDamage.fillAmount = (Player.Instance.GetPlayerDamage() - initialDamage) / (Player.maxPlayerDamage - initialDamage);
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
