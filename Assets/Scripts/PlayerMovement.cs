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

    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float wallJumpDirection = 3f;
    [SerializeField] private float attackDuration = 0.5f;
    [SerializeField] private float dashSpeed = 6f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float staminaTotal = 100;
    [SerializeField] private float staminaRecoverySpeed = 5;
    [Header("Skill")]
    [SerializeField] private bool canDoubleJump = false;
    [SerializeField] private bool canWallSlide = false;
    [SerializeField] private bool canDash = false;


    private MovementState state;
    private bool isGrounded;
    private bool isWallSliding;
    private bool isFacingRight;
    private float directX;  
    private float wallJumpCoolDown;
    private bool isJumping;
    private bool isAttacking = false;
    private float attack = 0f;
    private float dash = 0f;
    private bool isDashing = false;
    public int facingDirection = 1;
    public bool canMove = true;
    public float stamina;
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
        attack = attackDuration;
        dash = dashDuration;
        stamina = staminaTotal;
        attackDuration = 0;
        dashDuration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<PlayerLife>().IsDead() || !isAttacking || !isDashing)
        {
            //Debug.Log(canMove);
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
        directX = Input.GetAxisRaw("Horizontal");
        isWallSliding = IsTouchingWall() && canWallSlide;
        isGrounded = IsGrounded();


        attackDuration = Mathf.Clamp(attackDuration - Time.deltaTime, 0f,attack);      
        if (attackDuration > 0 )attackBackground.fillAmount = attackDuration / attack;
        dashDuration = Mathf.Clamp(dashDuration - Time.deltaTime, 0f,dash);
        if (dashDuration > 0) dashBackground.fillAmount = dashDuration / dash;

        if (!isDashing)
        {
            stamina = Mathf.Clamp(stamina + Time.deltaTime * staminaRecoverySpeed, 0, staminaTotal);
        }
        if (!isAttacking && !isDashing && canMove)
        {
        rb.velocity = new Vector2(directX * moveSpeed, rb.velocity.y);
        }
        if (isGrounded || isWallSliding)
        {          
        }
        
        if (isWallSliding && canWallSlide)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        if (canMove)
        {
            if (Input.GetButtonDown("Jump"))
            {   
                    Jump();                
            }else if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Attack());
            }else if(Input.GetKeyDown(KeyCode.LeftShift) && stamina >= 25)
            {
                StartCoroutine(Dash());
            }
        }
        UpdateAnimationMove();

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
        else if (isJumping && canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = false;
        }
        else if (isWallSliding && wallJumpCoolDown >= 1f)
        {
            if (IsTouchingLeftWall())
            {
                rb.velocity = new Vector2(wallJumpDirection, jumpForce);
            }
            else if (IsTouchingRightWall())
            {
                rb.velocity = new Vector2(-wallJumpDirection, jumpForce);
            }
            wallJumpCoolDown = 0;
        }
       

    }
    private IEnumerator Dash()
    {      
        if(canDash && dashDuration <= 0f)
        {
            stamina =  Mathf.Clamp(stamina - 25, 0, staminaTotal);           
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
        if (isWallSliding && !isGrounded && rb.velocity.y < -.1f)
        {
            state = MovementState.wallsliding;
        }
        else if (rb.velocity.y > .1f)
        {

            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;

        }
        anim.SetInteger("state", (int)state);
    }
    public float GetStamina()
    {
        return staminaTotal;
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