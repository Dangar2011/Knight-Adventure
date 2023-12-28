using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoneBoss : MonoBehaviour
{
    [SerializeField] private GameObject summon;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Collider2D coll;
    [SerializeField] private GameObject arm;

    [SerializeField] private float shieldCooldown = 30f;
    [SerializeField] private float healCooldown = 15f;
    [SerializeField] private float armAttackCooldown = 4f;
    [SerializeField] private float armShootCooldown = 15f;
    //[SerializeField] private float laserCooldown = 30f;
    [SerializeField] private float heightSummon = 20f;
    [SerializeField] private float attackRange = 2;
    [SerializeField] private float summonCooldown = 10f;
    [SerializeField] private float damage = 30;
    [SerializeField] private float shieldTime = 2f;
    private Animator anim;
    private PlayerLife playerLife;
    public bool isDead = false;
    private float maxHP;
    private float currentHP;
    private bool isShielded = false;
    private bool isHealing = false;
    private bool isArmAttacking = false;
    private bool isArmShoot = false;
    //public bool isLaser = false;
    private BossHealthBar healthBar;
    private Vector3 initialScale;
    private GameObject summonPosition;
    private GameObject armPosition;
    //private GameObject laserPosition;
    private GameObject player;
    private EnemyLife enemyLife;
    private float timeToShield;

    private float shieldDuration;
    private float healDuration;
    private float armAttackDuration;
    //private float laserDuration;
    private float armShootDuration;
    private float summonDuration;

    private void Awake()
    {
        

        initialScale = transform.localScale;
        
        shieldDuration = shieldCooldown;
        healDuration = healCooldown;
        armAttackDuration = armAttackCooldown;
        //laserDuration = laserCooldown;
        armShootDuration = armShootCooldown;
        summonDuration = summonCooldown;
        timeToShield = shieldTime;

        enemyLife = GetComponent<EnemyLife>();
    }
    void Start()
    {

        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        summonPosition = transform.Find("Summon Position").gameObject;
        armPosition = transform.Find("Arm Shoot Position").gameObject;
        //laserPosition = transform.Find("Laser Position").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
       
        maxHP = enemyLife.currentHP;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isDead = enemyLife.isDead;
        shieldDuration -= Time.deltaTime;
        healDuration -= Time.deltaTime;
        armAttackDuration -= Time.deltaTime;
        //laserDuration -= Time.deltaTime;
        armShootDuration -= Time.deltaTime;      
        summonDuration -= Time.deltaTime;
        enemyLife.isShielded = isShielded;
        currentHP = enemyLife.currentHP;
    }
    private void Update()
    {
        Debug.Log("shieldDuration" + shieldDuration);
        Debug.Log("healDuration" + healDuration);

        

        if (!isDead && !isShielded && !isHealing)
        {
            if (summonDuration < 0)
            {
                Summon();
            }
            else if (armAttackDuration < 0 && PlayerInsight() && !isArmShoot)
            {
                ArmAttack();
            }
            else if (armShootDuration < 0 && !isArmAttacking )
            {
                ArmShoot();
            }
            else if(shieldDuration < 0 && (currentHP < maxHP / 2))
            {
                 StartCoroutine(Shield());
            }
            else if(healDuration < 0 && (currentHP < maxHP / 3))
            {
                 StartCoroutine(Heal());
            }          
        }
        if (!isDead) 
        { 
            UpdateSummonPosition();
            PlayerDirection();
        }
    }

    private void EndArmAttack()
    {
        isArmAttacking = false;
    }
    private void ArmShoot()
    {
        isArmShoot = true;
        anim.SetTrigger("isArmShoot");
        armShootDuration = armShootCooldown;
    }
    private void EndArmShoot()
    {
        isArmShoot = false;
    }
    private void ArmShootAttack()
    {
        Instantiate(arm, armPosition.transform.position, Quaternion.identity);
    }
    private void ArmAttack()
    {
        anim.SetTrigger("isArmAttack");
        armAttackDuration = armAttackCooldown;
        isArmAttacking = true;

    }
    
    private void Summon()
    {
        Instantiate(summon, summonPosition.transform.position, Quaternion.identity);
        summonDuration = summonCooldown;
    }
   

    private IEnumerator Shield()
    {
        anim.SetBool("isShield", true);
        isShielded = true;
        shieldDuration = shieldCooldown;
        yield return new WaitForSeconds(5f);
        isShielded = false;
        anim.SetBool("isShield", false);

            
    }   
    private IEnumerator Heal()
    {
        isHealing = true;
        healDuration = healCooldown;
        anim.SetBool("isHealing", true);
        enemyLife.currentHP = Mathf.Clamp(currentHP + 300, 0, maxHP);
        yield return new WaitForSeconds(2f);
        enemyLife.healthBar.UpdateHealthBar(currentHP, maxHP);
        isHealing = false;
        anim.SetBool("isHealing", false);

    }
    private void UpdateSummonPosition()
    {
        Vector3 playerHeadPosition = player.transform.position + Vector3.up * heightSummon;
        summonPosition.transform.position = playerHeadPosition;

    }
    public void PlayerDirection()
    {       
            Vector3 targetPosition = player.transform.position;
            if (transform.position.x < targetPosition.x)
            {
                transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
            }        
    }
    private void DamagePlayer()
    {
        if (PlayerInsight())
        {
            if (transform.position.x < playerLife.transform.position.x)
            {
                PlayerLife.enemyPosition = 1;
            }
            else
            {
                PlayerLife.enemyPosition = -1;
            }
            StartCoroutine(playerLife.TakeDamage(damage));
        }

    }
    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center + transform.right * attackRange * transform.localScale.x,
            new Vector3(coll.bounds.size.x * attackRange, coll.bounds.size.y, coll.bounds.size.z), 0.1f, Vector2.right, 0, playerLayer);
        if (hit.collider != null)
        {
            playerLife = hit.transform.GetComponent<PlayerLife>();
        }
        return hit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x < collision.transform.position.x)
        {
            PlayerLife.enemyPosition = 1;
        }
        else
        {
            PlayerLife.enemyPosition = -1;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(collision.GetComponent<PlayerLife>().TakeDamage(damage));

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(coll.bounds.center + transform.right * attackRange * transform.localScale.x,
           new Vector3(coll.bounds.size.x * attackRange, coll.bounds.size.y, coll.bounds.size.z));
    }
}
