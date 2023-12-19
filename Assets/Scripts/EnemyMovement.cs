using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    //[SerializeField] private Transform left;
    //[SerializeField] private Transform right;
    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;
    [SerializeField] private float idle = 1.5f;
    private Animator anim;
    private Vector3 initialScale;
    private EnemyLife enemyLife;
    private MovementState state;
    private bool findPlayer = false;
    //private bool isMovingRight = true;
    //private float idle;
    private float initialSpeed;
    private bool isMove = true;

    [SerializeField] private GameObject[] waypoints;
    private int currentWaypoint = 0;
    private enum MovementState
    {
        idle, running
    }
    void Start()
    {
        initialScale = enemy.localScale;
        anim = enemy.GetComponent<Animator>();
        enemyLife = enemy.GetComponent<EnemyLife>();       
        initialSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.GetComponent<EnemyLife>().IsDead() || enemyLife.isAttacking || enemyLife.isSummoning)
        {
            speed = 0;
        }
        else if (findPlayer)
        {
            speed = initialSpeed + 2;
        }
        else
        {
            speed = initialSpeed;
            //}
            //if (findPlayer)
            //{

            //}
            //else
            //{
            //if (isMovingRight)
            //{
            //    if (enemy.position.x <= right.position.x)
            //        Move(1);
            //    else
            //        ChangeDirect();
            //}
            //else
            //{
            //    if (enemy.position.x >= left.position.x)
            //        Move(-1);
            //    else
            //        ChangeDirect();
            //}
            MoveToPoint();
        }

        anim.SetInteger("state", (int)state);
    }
    private IEnumerator ChangeDirect()
    {
        isMove = false;
        state = MovementState.idle;
        //idle -= Time.deltaTime;
        yield return new WaitForSeconds(idle);
        currentWaypoint++;
        isMove = true;
        //    isMovingRight = !isMovingRight;
    }
    private void MoveToPoint()
    {
        
        if (currentWaypoint >= waypoints.Length)
        {
            currentWaypoint = 0;
        }
        if (enemy.position.x < waypoints[currentWaypoint].transform.position.x)
        {
            enemy.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z); ;
        }
        else
        {
            enemy.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z); ;
        }

        if (Vector2.Distance(waypoints[currentWaypoint].transform.position, enemy.position) < .1f)
        {
            if (isMove)
            {
               StartCoroutine(ChangeDirect());                
            }
        }

        if (currentWaypoint >= waypoints.Length)
        {
            currentWaypoint = 0;
        }
       
       
        if (isMove)
        {
            state = MovementState.running;
            enemy.position = Vector2.MoveTowards(enemy.position, waypoints[currentWaypoint].transform.position, Time.deltaTime * speed);
        }
    }
    //private void Move(int directX)
    //{
    //    idle = idleDuration;
    //    state = MovementState.running;

    //    enemy.localScale = new Vector3(directX * initialScale.x, initialScale.y, initialScale.z);

    //    enemy.position = new Vector3(enemy.position.x + Time.deltaTime * directX * speed,
    //        enemy.position.y, enemy.position.z);
    //}
    public void PlayerNotFound()
    {
        findPlayer = false;
    }
    public void MoveToPlayer()
    {
        state = MovementState.running;
        findPlayer = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position;

            Vector3 targetPositionHorizontal = new Vector3(targetPosition.x - 2f * enemy.localScale.x, enemy.position.y, enemy.position.z);

            float distanceToTarget = Vector3.Distance(enemy.position, targetPositionHorizontal);

            if (distanceToTarget > 0.1f)
            {
                enemy.position = Vector3.MoveTowards(enemy.position, targetPositionHorizontal, speed * Time.deltaTime);

                if (enemy.position.x < targetPositionHorizontal.x)
                {
                    enemy.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
                }
                else
                {
                    enemy.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
                }
            }
        }
    }



    private void OnDisable()
    {
        state = MovementState.idle;
    }
}
