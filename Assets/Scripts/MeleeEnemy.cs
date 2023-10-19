using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField]private BoxCollider2D coll;
    [SerializeField] private float attackDuration = 1f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage = 20;
    private Animator anim;
    private PlayerLife playerLife;
    private EnemyMovement enemyMovement;
    private float coolDown;
    private int enemySide = 1;
    private bool canMove = true;

    void Start()
    {
        coolDown = attackDuration;
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        enemyMovement = GetComponentInParent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
       if (canMove)
        {
            enemyMovement.enabled = true;
        }
        else
        {
            enemyMovement.enabled = false;
        }
        coolDown -= Time.deltaTime;
        if (PlayerInsight())
        {
            if(coolDown < 0)
            {
                anim.SetTrigger("isAttack");
                coolDown = attackDuration;
                canMove = false;
            }
        }
      
        if (enemyMovement != null)
        {
            enemyMovement.enabled = !PlayerInsight();
            
        }
        if (enemyMovement.enabled == false ) anim.SetInteger("state", 0);
    }
    public void EndAttack()
    {
        canMove = true;
    }

    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center + transform.right * attackRange * transform.localScale.x,
            new Vector3(coll.bounds.size.x* attackRange, coll.bounds.size.y, coll.bounds.size.z), 0.1f, Vector2.right, 0, playerLayer);
        if (hit.collider != null)
        {
            playerLife = hit.transform.GetComponent<PlayerLife>();
        }
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(coll.bounds.center + transform.right * attackRange * transform.localScale.x,
           new Vector3(coll.bounds.size.x * attackRange, coll.bounds.size.y, coll.bounds.size.z));

    }
    private void DamagePlayer()
    {
        if (PlayerInsight())
        {
            if (transform.position.x < playerLife.transform.position.x)
            {
                enemySide = 1;
            }
            else
            {
                enemySide = -1;
            }
            playerLife.GetEnemySide(enemySide);
            StartCoroutine(playerLife.TakeDamage(damage));
        }
    }
}
