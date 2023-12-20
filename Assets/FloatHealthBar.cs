using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatHealthBar : MonoBehaviour
{
    [SerializeField]private Slider slider;
    [SerializeField]private Transform enemy;

    public void UpdateHealthBar(float currentHP,float maxHP)
    {
        slider.value = currentHP / maxHP;
    }
    // Update is called once per frame when
    void Update()
    {
        if(enemy != null)
        {

        transform.position = enemy.position + Vector3.up;
        }
        else
        {
            Destroy(gameObject);
        }
        transform.rotation = Camera.main.transform.rotation;
    }
}
