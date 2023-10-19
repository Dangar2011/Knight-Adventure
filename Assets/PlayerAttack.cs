using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackRange = 3;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] public int damage { get; private set; } = 20;
    [SerializeField]private BoxCollider2D coll;
    private EnemyLife enemyLife;
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private bool EnemyInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center + transform.right * attackRange * transform.localScale.x,
            new Vector3(coll.bounds.size.x * attackRange, coll.bounds.size.y, coll.bounds.size.z), 0.1f, Vector2.right, 0, enemyLayer);
        if (hit.collider != null)
        {
            enemyLife = hit.transform.GetComponent<EnemyLife>();
        }
        return hit.collider != null;
    }
    private void DamageEnemy()
    {
        if (EnemyInsight())
        {

            enemyLife.TakeDamage(damage);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(coll.bounds.center + transform.right * attackRange * transform.localScale.x,
           new Vector3(coll.bounds.size.x * attackRange, coll.bounds.size.y, coll.bounds.size.z));

    }
}
