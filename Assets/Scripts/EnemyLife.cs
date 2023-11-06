using System.Collections;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    public int currentHP { get; private set; }
    private Animator anim;
    private EnemyMovement enemyMovement;
    void Start()
    {
        currentHP = maxHP;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Die()
    {
        //deathSound.Play();
        
        anim.SetTrigger("isDeath");
        Debug.Log(anim.GetCurrentAnimatorClipInfo(0).Length);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);       
        Destroy(gameObject);


    }
    public void TakeDamage(int damage)
    {
            currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
            if (currentHP > 0)
            {
                anim.SetTrigger("isTakeHit");

            }
            else
            {
                StartCoroutine(Die());
            }
    }
}
