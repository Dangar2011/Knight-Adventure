using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;
    [SerializeField] private float idleDuration = 1.5f;
    private Animator anim;
    private Vector3 initialScale;
    private MovementState state;
    
    private bool isMovingRight = true;
    private float idle;
    public bool isAttacking = false;
    private enum MovementState
    {
        idle,running
    }
    void Start()
    {
        initialScale = enemy.localScale;
        anim = enemy.GetComponent<Animator>();
        idle = idleDuration;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isMovingRight)
        {
            if (enemy.position.x <= right.position.x)
                Move(1);
            else            
                ChangeDirect();            
        }    
        else 
        {
            if (enemy.position.x >= left.position.x)
                Move(-1);
            else
                ChangeDirect();
        }
        anim.SetInteger("state", (int)state);
    }
    private void ChangeDirect()
    {
        state = MovementState.idle;
        idle -=Time.deltaTime;
        if(idle < 0)
            isMovingRight = !isMovingRight;
    }
    private void Move(int directX)
    {
        idle = idleDuration;
        state = MovementState.running;
        
        enemy.localScale = new Vector3 (directX,initialScale.y, initialScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * directX * speed,
            enemy.position.y,enemy.position.z);
    }
    private void OnDisable()
    {
        state = MovementState.idle;
    }
}
