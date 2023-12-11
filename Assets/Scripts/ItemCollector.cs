using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{

    private PlayerLife playerLife;
    void Start()
    {
        playerLife = GetComponent<PlayerLife>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Coin"))
        {
            FinishPoint.coin++;
            Destroy(coll.gameObject);

        };
        if (coll.gameObject.CompareTag("Fruit"))
        {
            playerLife.Heal();
        }
        if (coll.gameObject.CompareTag("Heart"))
        {
            playerLife.CollectHeart();
        }
    }
}
