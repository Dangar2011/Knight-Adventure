using System.Collections;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private float maxHP = 100;
    private Animator anim;
    private EnemyMovement enemyMovement;

    public static bool isAttacking = false;
    public static bool isSummoning = false;
    public float currentHP { get; private set; } = 100;   
    public float enemyCoin { get; private set; } = 1f;
    public bool isDead = false;
    void Start()
    {
        currentHP = maxHP;
        anim = GetComponent<Animator>();
        enemyMovement = GetComponentInParent<EnemyMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Die()
    {
        //deathSound.Play();
        FinishPoint.coin += enemyCoin;
        isDead = true;
        anim.SetTrigger("isDeath");
     
    }
    public void Disappear()
    {
        Destroy(transform.parent.gameObject);
    }
    public bool IsDead()
    {
        return isDead;
    }
    public void TakeDamage(float damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
        Debug.Log(isAttacking);
        if (currentHP > 1 && !isAttacking && !isSummoning)
        {
                anim.SetTrigger("isTakeHit");     
        }
        else if(currentHP == 0)
        {
            
            Die();
        }
       
    }
}
