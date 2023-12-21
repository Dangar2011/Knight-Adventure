using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            AudioManager.Instance.PlaySFX("Collect Fruit");
            playerLife.Heal(HPHeal);
            coll.GetComponent<Animator>().SetBool("isCollected", true);
            coll.GetComponent<Collider2D>().enabled = false;
            float animationDuration = coll.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(DelayDestroyObj(coll.gameObject, animationDuration-0.5f));
        }
        if (coll.gameObject.CompareTag("Heart"))
        {
            AudioManager.Instance.PlaySFX("Collect Heart");
            playerLife.CollectHeart(); 
            coll.GetComponent<Animator>().SetBool("isCollected", true);
            coll.GetComponent<Collider2D>().enabled = false;
            float animationDuration = coll.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(DelayDestroyObj(coll.gameObject, animationDuration - 0.5f));
        }
    }
    private IEnumerator DelayDestroyObj(GameObject gameObject,float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        Destroy(gameObject);
    }
}
