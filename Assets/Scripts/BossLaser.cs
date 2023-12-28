using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    // Start is called before the first frame update
    //private bool isLaser = false;
    private bool isCheckPlayer = false;
    private Transform player;
    private Vector3 playerPosition;
    private Collider2D coll;
    private SpriteRenderer sprite;
    private StoneBoss stoneBoss;
    void Start()
    {
        stoneBoss =GetComponentInParent<StoneBoss>();
        coll = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCheckPlayer)
        {
            playerPosition = player.position;
            RotateTowardsPlayer();
            isCheckPlayer = false;
        }

    }
    private void BeamShoot()
    {
        
    }
    private void LaserStart()
    {
        isCheckPlayer = true;
    }
    private void LaserEnd()
    {
        //stoneBoss.isLaser = false;
        GetComponent<Animator>().SetTrigger("isLaserEnd");
        coll.enabled = false;
    }
    private void RotateTowardsPlayer()
    {
        Vector3 direction = playerPosition - transform.position;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }
}
