using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy
{
    public float speed;
    private Rigidbody2D rb;
    public Transform toppoint, bottompoint;
    private float topy, bottomy;
    private bool isUp = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        topy = toppoint.position.y;
        bottomy = bottompoint.position.y;
        transform.DetachChildren();
        Destroy(toppoint.gameObject);
        Destroy(bottompoint.gameObject);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        if (isUp)
        {
            if (transform.position.y > topy)
            {
                isUp = false;
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, speed);
            }
        }
        else
        {
            if (transform.position.y < bottomy)
            {
                isUp = true;
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -speed);
            }
        }
    }
}
