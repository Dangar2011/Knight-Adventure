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
    [SerializeField] private Image currentATK;
    private float initialHP;
    private float initialSpeed;
    private float initialATK;
    private float currentCoin = 0;
    private float coinToUpHP = 100f;
    private float coinToUpSpeed = 100f;
    private float coinToUpATK = 100f;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI coinToUpHPText;
    public TextMeshProUGUI coinToUpSpeedText;
    public TextMeshProUGUI coinToUpATKText;
    public Button upgradeHP;
    public Button upgradeSpeed;
    public Button upgradeATK;
    void Start()
    { 
        GetCoinToUp();
        player = GetComponent<Player>();    
        initialHP = 100f;
        initialSpeed = 5f;
        initialATK = 10f;
        coinToUpHPText.text = coinToUpHP.ToString();
        coinToUpSpeedText.text = coinToUpSpeed.ToString();
        coinToUpATKText.text = coinToUpATK.ToString();
        currentHP.fillAmount = (player.GetPlayerHP()) / player.maxPlayerHP;
        currentSpeed.fillAmount = (player.GetPlayerSpeed() - initialSpeed) / player.maxPlayerSpeed;
        currentATK.fillAmount = (player.GetPlayerDamage() - initialATK) / player.maxPlayerDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetPlayerCoin() >= coinToUpHP) upgradeHP.interactable = true;
        else upgradeHP.interactable = false;
        if (player.GetPlayerCoin() >= coinToUpSpeed) upgradeSpeed.interactable = true;
        else upgradeSpeed.interactable = false;
        if (player.GetPlayerCoin() >= coinToUpATK) upgradeATK.interactable = true;
        else upgradeATK.interactable = false;
        currentCoin = player.GetPlayerCoin();
        coinText.text = currentCoin.ToString();
        
    }

    public void Cheat()
    {
        player.SetPlayerCoin(1000);
    }
    public void DeleteKey()
    {
        PlayerPrefs.DeleteKey("coinHP");
    }
    public void DeleteAllKey()
    {
        PlayerPrefs.DeleteKey("PlayerHP");
        PlayerPrefs.DeleteKey("PlayerDamage");
        PlayerPrefs.DeleteKey("PlayerSpeed");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    public void UpgradeHP()
    {
        if(player.GetPlayerCoin() >= coinToUpHP)
        {
            player.SetPlayerCoin(-coinToUpHP);
            coinToUpHP = Mathf.Clamp(coinToUpHP + 50, 0, 500);
            coinToUpHPText.text = coinToUpHP.ToString();
            player.SetPlayerHP();
            Debug.Log(player.GetPlayerHP());
            currentHP.fillAmount = (player.GetPlayerHP()) / player.maxPlayerHP;
            SetCoinToUp();
        }
    }
    public void UpgradeSpeed()
    {
        if (player.GetPlayerCoin() >= coinToUpSpeed)
        {
            player.SetPlayerCoin(-coinToUpSpeed);
            coinToUpSpeed = Mathf.Clamp(coinToUpSpeed + 50, 0,500);
            coinToUpSpeedText.text = coinToUpSpeed.ToString();
            player.SetPlayerSpeed();
            currentSpeed.fillAmount = (player.GetPlayerSpeed() - initialSpeed) / player.maxPlayerSpeed;
            SetCoinToUp();
        }
    }
    public void UpgradeATK()
    {
        if (player.GetPlayerCoin() >= coinToUpATK)
        {
            player.SetPlayerCoin(-coinToUpATK);
            coinToUpATK = Mathf.Clamp(coinToUpATK + 50, 0, 500);
            coinToUpATKText.text = coinToUpATK.ToString();
            player.SetPlayerDamage();
            currentATK.fillAmount = (player.GetPlayerDamage() - initialATK) / player.maxPlayerDamage;
            SetCoinToUp();
        }
    }
    public void SetCoinToUp()
    {
        PlayerPrefs.SetFloat("coinHP",coinToUpHP);
        PlayerPrefs.SetFloat("coinSpeed", coinToUpSpeed);
        PlayerPrefs.SetFloat("coinATK", coinToUpATK);
        PlayerPrefs.Save();
    }
    public void GetCoinToUp()
    {
        coinToUpHP = PlayerPrefs.GetFloat("coinHP",100f);
        coinToUpSpeed = PlayerPrefs.GetFloat("coinSpeed",100f);
        coinToUpATK = PlayerPrefs.GetFloat("coinATK",100f);
    }
}

