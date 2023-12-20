using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private float HPHeal = 10f;
    private PlayerLife playerLife;
    void Start()
    {
        playerLife = GetComponent<PlayerLife>();
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Coin"))
        {
            FinishPoint.coin++;
            AudioManager.Instance.PlaySFX("CollectCoin");
            Destroy(coll.gameObject);

        };
        if (coll.gameObject.CompareTag("Fruit"))
        {
            AudioManager.Instance.PlaySFX("CollectFruit");
            playerLife.Heal(HPHeal);
            Destroy(coll.gameObject);
        }
        if (coll.gameObject.CompareTag("Heart"))
        {
            AudioManager.Instance.PlaySFX("CollectHeart");
            playerLife.CollectHeart();
            Destroy(coll.gameObject);
        }
    }
}
