using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    private Rigidbody2D rb;
    public Transform leftpoint, rightpoint;
    private Collider2D coll;
    public float speed,jumpForce;
    private bool faceLeft = true;
    private float leftx, rightx;
   // private Animator anime;
    public LayerMask ground;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anime = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        SwitchAnime();
    }
    void Movement()
    {
        if (faceLeft)
        {
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
            if (coll.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(-speed, jumpForce);
                anime.SetBool("Jumping", true);
            }
          

        }
        else
        {
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
            if (coll.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(speed, jumpForce);
                anime.SetBool("Jumping", true);
            }
            
        }
    }
    void SwitchAnime()
    {
        if (anime.GetBool("Jumping"))
        {
            if (rb.velocity.y < 0.1)
            {
                anime.SetBool("Jumping", false);
                anime.SetBool("Falling", true);
            }
        }
        if (coll.IsTouchingLayers(ground) && anime.GetBool("Falling"))
        {
            anime.SetBool("Falling", false);
        }
    }

 
 }
