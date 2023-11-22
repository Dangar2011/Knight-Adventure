using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerLife : MonoBehaviour
{
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private float invicibilityTime = 0.35f;
    [SerializeField] private float bounceBack = 5f;
    [SerializeField] private float respawnTime = 2f;
    private PlayerMovement playerMovement;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Vector2 respawnPosition;
    private TextMeshProUGUI hpText;

    private int life = 3;
    private float playerHP;
    public float currentHP { get; private set; }
    public int currentLife { get; private set; }
    private bool isDead = false;
    private float flashDuration;
    private int enemyPosition;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        sprite = GetComponent<SpriteRenderer>();
        playerHP = GetComponent<Player>().GetPlayerHP();
        respawnPosition = transform.position;
        currentHP = playerHP;
        flashDuration = invicibilityTime;
        currentLife = life;
        
    }
    private void Update()
    {
        invicibilityTime = Mathf.Clamp(invicibilityTime - Time.deltaTime, 0, flashDuration);
       
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //if (collision.gameObject.CompareTag("DeathZone"))
        //{

        //    Die();
        //}

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
           
        }
    }
    private IEnumerator Die()
    {
        //deathSound.Play();
        playerMovement.canMove = false;
        anim.SetTrigger("isDeath");             
        isDead = true;
        currentLife = currentLife - 1;
        if (currentLife == 0)
        {
            yield return new WaitForSeconds(1f);
            LevelFail();

        }
        else
        {
            yield return new WaitForSeconds(respawnTime);
            RespawnPlayer();
        }
    }
    public IEnumerator TakeDamage(float damage)
    {
        if (invicibilityTime == 0)
        {
            
            currentHP = Mathf.Clamp(currentHP - damage, 0, playerHP);
            if (currentHP > 0 && !isDead)
            {
                anim.SetTrigger("isTakeHit");
                StartCoroutine(FlashCharacter());
                playerMovement.canMove = false;
                rb.velocity = new Vector2(enemyPosition * bounceBack, bounceBack * 1.7f);
                invicibilityTime = flashDuration;
                yield return new WaitForSeconds(0.3f);
                playerMovement.canMove = true;
            }
            else
            {
                yield return null;
                StartCoroutine(Die());
            }
        }
        else
        {

        }
    }
    private IEnumerator FlashCharacter()
    {
       
        float flashInterval = 0.1f;
        float timer = 0f;

        while (timer < flashDuration)
        {            
            sprite.enabled = !sprite.enabled;        
            yield return new WaitForSeconds(flashInterval);
            timer += flashInterval;
        }
        sprite.enabled = true;
        
    }
    public bool IsDead()
    {
        return isDead;
    }
    private void RespawnPlayer()
    {
        anim.SetTrigger("respawn");
        playerMovement.canMove = true;
        transform.position = respawnPosition;
        currentHP = playerHP;
        playerMovement.currentMP = GetComponent<Player>().GetPlayerMP(); 
        anim.SetInteger("state", 0);
        isDead = false;


    }
    public int GetLife()
    {
        return life;
    }
    public void GetEnemySide(int value)
    {
        enemyPosition = value;
    }
    public void UpdateCheckpoint(Vector2 checkpointPosition)
    {
        respawnPosition = checkpointPosition;
    }
    private void LevelFail()
    {
        Debug.Log("fail");
    }
    public void Heal(float value)
    {
        currentHP += value;
    }
    public void CollectHeart()
    {
        currentLife++;
    }
    public void IncreaseHP(int value)
    {
        playerHP += value;
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
