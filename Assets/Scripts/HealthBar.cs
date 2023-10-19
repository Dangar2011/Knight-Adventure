using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerLife playerLife;
    
    [SerializeField] private Image healthBar;
    [SerializeField] private Image staminaBar;
    [SerializeField] private Image lifeBar;
    private PlayerMovement player;
    void Start()
    {
        player = playerLife.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = playerLife.currentHP / playerLife.GetHP();
        staminaBar.fillAmount = player.stamina / player.GetStamina();
        lifeBar.fillAmount = (float)playerLife.currentLife / (float)playerLife.GetLife();

    }


}
