using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private TrailRenderer tr;



    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private TextMeshProUGUI attackCoolDown;
    [SerializeField] private Image attackBackground;
    [SerializeField] private Image dashBackground;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 14f;

    [SerializeField] private float dashSpeed = 6f;

    [SerializeField] private float recoverySpeedMP = 25f;
    private float playerMP;


    private MovementState state;
    private bool isGrounded;
    private float directX;  
    public float currentMP;

    private bool isFacingRight;
    public int facingDirection = 1;

    private bool isJumping;
    private float dashDuration;
    private float attackDuration;
    private bool isAttacking = false;
    private bool isDashing = false;
    private float attack = 0f;
    private float dash = 0f;
    public bool canMove = true;

    private enum MovementState
    {
        idle, running, jumping, falling, doublejumping, wallsliding,dashing
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        tr = GetComponent<TrailRenderer>();
        playerMP = GetComponent<Player>().GetPlayerMP();
        attack = attackDuration;
        dash = dashDuration;
        currentMP = playerMP;
        attackDuration = 0;
        dashDuration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        directX = Input.GetAxisRaw("Horizontal");
        if (!GetComponent<PlayerLife>().IsDead() || !isAttacking || !isDashing)
        {
            if (directX > 0f)
            {
                isFacingRight = true;
                facingDirection = 1;
            }
            else if (directX < 0f)
            {
                isFacingRight = false;
                facingDirection = -1;
            }
        }
        isGrounded = IsGrounded();

        
        UpdateAnimationMove();
        CanMove();

    }
    void FixedUpdate()
    {
        attackDuration = Mathf.Clamp(attackDuration - Time.deltaTime, 0f, attack);
        if (attackDuration > 0) attackBackground.fillAmount = attackDuration / attack;
        dashDuration = Mathf.Clamp(dashDuration - Time.deltaTime, 0f, dash);
        if (dashDuration > 0) dashBackground.fillAmount = dashDuration / dash;

        if (!isDashing)
        {
            Debug.Log("TIme "+Time.deltaTime);
            Debug.Log(Time.deltaTime * 5);
            currentMP = Mathf.Clamp(currentMP + Time.deltaTime * recoverySpeedMP, 0, playerMP);
        }

        if (canMove)
        {
            rb.velocity = new Vector2(directX * moveSpeed, rb.velocity.y);

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Attack());
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && currentMP >= 25)
            {
                StartCoroutine(Dash());
            }
        }
    }
    private void CanMove()
    {
        canMove = !GetComponent<PlayerLife>().IsDead() || !isAttacking || !isDashing; 
    }
    private IEnumerator Attack()
    {
        if(attackDuration <= 0f)
        {            
            isAttacking = true;
            anim.SetTrigger("isAttack");
            attackDuration = attack;
            yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length );
            isAttacking = false;            
        }
    
    }

    void Jump()
    {
        if (isGrounded)
        {            
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
        }
        else if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = false;
        }     
       

    }
    private IEnumerator Dash()
    {      
        if(dashDuration <= 0f)
        {
            currentMP =  Mathf.Clamp(currentMP - 25, 0, playerMP);           
            isDashing = true;
            
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            float dashVelocity = dashSpeed * facingDirection;
            rb.velocity = new Vector2(dashVelocity,0f);
            anim.SetTrigger("isDash");
            dashDuration = dash;
            tr.emitting = true;
            yield return new WaitForSeconds(0.2f);
            tr.emitting = false;
            rb.gravityScale = originalGravity;
            isDashing = false;
            
        }
       
    }

    private void UpdateAnimationMove()

    {

        //if (directX > 0f)
        //{
        //    state = MovementState.running;
        //    sprite.flipX = false;

        //}
        //else if (directX < 0f)
        //{
        //    state = MovementState.running;
        //    sprite.flipX = true;

        //}
        if (directX != 0f)
        {
           
            state = MovementState.running;
            sprite.flipX = !isFacingRight;
        }
        else
        {
            state = MovementState.idle;

        }      
        if (rb.velocity.y > .1f)
        {

            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;

        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }
    private bool IsTouchingWall()
    {

        return (IsTouchingLeftWall() || IsTouchingRightWall());
    }
    private bool IsTouchingRightWall()
    {

        return Physics2D.Raycast(coll.bounds.center, Vector2.right * directX, coll.bounds.extents.x + 0.1f, wallLayer);
    }
    private bool IsTouchingLeftWall()
    {
        return Physics2D.Raycast(coll.bounds.center, Vector2.left * directX, coll.bounds.extents.x + 0.1f, wallLayer);

    }

}