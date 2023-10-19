using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private int enemySide = 1;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x < collision.transform.position.x)
        {
            enemySide = 1;
        }
        else
        {
            enemySide = -1;
        }
        if (collision.gameObject.CompareTag("Player"))
        {           
            collision.GetComponent<PlayerLife>().GetEnemySide(enemySide);
           StartCoroutine(collision.GetComponent<PlayerLife>().TakeDamage(damage));

        }
    }

}
