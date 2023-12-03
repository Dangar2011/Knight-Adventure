using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SummonEnemy : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private Animator anim;
    public GameObject summonPosition;
    public GameObject player;
    [SerializeField] private GameObject summon;
    [SerializeField] private BoxCollider2D coll;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float summonDuration = 10f;
    [SerializeField] private float summonRange = 5f;
    public float heightSummon = 10f;
    private bool isSummoning = false;
    private float coolDown;
    void Start()
    {
        coolDown = summonDuration;
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        enemyMovement = GetComponentInParent<EnemyMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
        summonPosition = transform.GetChild(0).gameObject;
    }

    void Update()
    {
       

        coolDown -=Time.deltaTime;
        enemyMovement.isSummoning= isSummoning;      
        if (PlayerInsight() && !GetComponent<EnemyLife>().IsDead())
        {

            Vector3 playerHeadPosition = player.transform.position + Vector3.up * heightSummon; 
            summonPosition.transform.position = playerHeadPosition;
            if (coolDown < 0 && !enemyMovement.isAttacking)
            {
                isSummoning = true;
                anim.SetTrigger("isSummon");
                coolDown = summonDuration;
            }
                       
        }
    }
    public void Summon()
    {              
                Instantiate(summon, summonPosition.transform.position,Quaternion.identity );
    }
    public void EndSummon()
    {
        isSummoning = false;
    }
    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center + transform.right * summonRange * transform.localScale.x,
            new Vector3(coll.bounds.size.x * summonRange, coll.bounds.size.y, coll.bounds.size.z), 0.1f, Vector2.right, 0, playerLayer);
        //if (hit.collider != null)
        //{
        //    playerLife = hit.transform.GetComponent<PlayerLife>();
        //}
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(coll.bounds.center + transform.right * summonRange * transform.localScale.x,
           new Vector3(coll.bounds.size.x * summonRange, coll.bounds.size.y, coll.bounds.size.z));

    }
}