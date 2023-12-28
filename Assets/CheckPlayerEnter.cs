using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerEnter : MonoBehaviour
{
    private GameObject boss;
    private GameObject healthBar;
    public static bool isChecked;
    private void Awake()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        healthBar = GameObject.FindGameObjectWithTag("Boss HealthBar");
        isChecked = false;
        healthBar.SetActive(false);
    }
    void Start()
    {
        boss.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isChecked = true;
            boss.SetActive(true);
            healthBar.SetActive(true);
        }
    }

}
