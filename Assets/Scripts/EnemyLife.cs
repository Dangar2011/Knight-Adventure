using System.Collections;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private float maxHP = 100;
    private Animator anim;
    private Collider2D coll;
    public  bool isAttacking = false;
    public  bool isSummoning = false;
    public float currentHP { get; private set; } = 100;   
    public float enemyCoin { get; private set; } = 1f;
    public bool isDead = false;
    private bool hasDisappeared = false;
    private float timeDisappear = 0.3f;
    private float fadeTime = 0f;
    [SerializeField] private bool isEnemyFlight = false;
    void Start()
    {
        currentHP = maxHP;
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasDisappeared)
        {
            if (isEnemyFlight)
            {
                //float fadeDelayElapse = 0f;

                Debug.Log(transform.parent.name);
                SpriteRenderer sprite = GetComponent<SpriteRenderer>();

                    fadeTime += Time.deltaTime;
                    float newAlpha = sprite.color.a * (1 - (fadeTime / timeDisappear));
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, newAlpha);
                   
                    if (fadeTime > timeDisappear)
                    {
                        Debug.Log("acc");
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
        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
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
