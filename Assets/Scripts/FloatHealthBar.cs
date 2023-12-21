using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatHealthBar : MonoBehaviour
{
    [SerializeField]private Transform enemy;
    [SerializeField]private Slider slider;

    private void Start()
    {       
    }
    public void UpdateHealthBar(float currentHP,float maxHP)
    {
        slider.value = currentHP / maxHP;
    }
    // Update is called once per frame when
    void Update()
    {
        if(enemy != null)
        {

        transform.position = enemy.position + Vector3.up*2f;
        }
        else
        {
            Destroy(gameObject);
        }
        transform.rotation = Camera.main.transform.rotation;
        Vector3 healthBarLocalScale = new Vector3(enemy.localScale.x,1,1);
        transform.localScale = healthBarLocalScale;  
    }
}
