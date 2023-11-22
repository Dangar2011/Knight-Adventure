using System.Collections;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    private Animator anim;
    private EnemyMovement enemyMovement;
    public int currentHP { get; private set; } = 100;   
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
    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
        Debug.Log("isSummon: "+enemyMovement.isSummoning);
        Debug.Log("isAttacking: " + enemyMovement.isAttacking);
        if (currentHP > 1 && !enemyMovement.isAttacking && !enemyMovement.isSummoning)
        {
                anim.SetTrigger("isTakeHit");     
        }
        else if(currentHP == 0)
        {
            
            Die();
        }
       
    }
}
