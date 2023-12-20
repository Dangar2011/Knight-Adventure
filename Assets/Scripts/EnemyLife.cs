using System.Collections;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    private Animator anim;
    private Collider2D coll;
    private FloatHealthBar healthBar;

    [SerializeField] private float maxHP = 100;
    [SerializeField] private bool isEnemyFlight = false;

    public  bool isAttacking = false;
    public  bool isSummoning = false;
    public bool isShielded = false;
    public float currentHP { get; private set; }  
    public float enemyCoin = 1f;
    public bool isDead = false;
    private bool hasDisappeared = false;
    private float timeDisappear = 0.3f;
    private float fadeTime = 0f;
    private void Awake()
    {
         healthBar = GetComponentInChildren<FloatHealthBar>();
    }
    void Start()
    {
        currentHP = maxHP;
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        healthBar.UpdateHealthBar(currentHP, maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        if(hasDisappeared)
        {
            if (isEnemyFlight)
            {
                SpriteRenderer sprite = GetComponent<SpriteRenderer>();

                    fadeTime += Time.deltaTime;
                    float newAlpha = sprite.color.a * (1 - (fadeTime / timeDisappear));
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, newAlpha);
                   
                    if (fadeTime > timeDisappear)
                    {
                        Destroy(transform.parent.gameObject);
                    }
                
            }
            else
            {
                Destroy(transform.parent.gameObject);
            }

        }
    }
    private void Die()
    {
        //deathSound.Play();
        FinishPoint.coin += enemyCoin;
        isDead = true;
        anim.SetTrigger("isDeath");
        coll.enabled = false;
        if (isEnemyFlight)
        {
            Collider2D deathCollider = GetComponentInChildren<Collider2D>();
            Rigidbody2D rb = GetComponentInChildren<Rigidbody2D>();
            rb.gravityScale = 2f;
            rb.velocity = new Vector2(0, rb.velocity.y);
            deathCollider.enabled = true;
        }
     
    }
    public void Disappear()
    {
       hasDisappeared = true;
    }
    public bool IsDead()
    {
        return isDead;
    }
    public void TakeDamage(float damage)
    {
        if (!isShielded)
        {
            currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
            healthBar.UpdateHealthBar(currentHP,maxHP);
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
}
