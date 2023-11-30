using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadSummon : MonoBehaviour
{

    private Animator anim;
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    public float speed;
    //private GameObject player;


    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        rb =   GetComponent<Rigidbody2D>();
        //player = GameObject.FindGameObjectWithTag("Player");
        //Vector3 direction = player.transform.position - transform.position;
        //rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        //float rot = Mathf.Atan2(-direction.x, -direction.y) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }
    void Update()
    {
        Vector2 fallVelocity = new Vector2(0, -speed); 
        if(rb.bodyType == RigidbodyType2D.Dynamic)
            rb.velocity = fallVelocity;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("Idle",false );
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetTrigger("isHit");
            
        }
    }

    public void DestroySummon()
    {
        Destroy(gameObject);
    }
    public void Appear()
    {
        anim.SetBool("Idle", true);
    }
}
