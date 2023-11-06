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
    private IEnumerator Die()
    {
        //deathSound.Play();
        isDead = true;
        anim.SetTrigger("isDeath");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length+1f);       
        Destroy(transform.parent.gameObject);


    }
    public bool IsDead()
    {
        return isDead;
    }
    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
        if (currentHP > 0 && !enemyMovement.isAttacking)
        {
                anim.SetTrigger("isTakeHit");     
        }
        else if(currentHP == 0)
        {
            StartCoroutine(Die());
        }
       
    }
}
