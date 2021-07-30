using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public Transform cellingCheck;
    public AudioSource jumpAudio,cherryAudio,hurtAudio;
    private Rigidbody2D rb;
    private Animator anime;
    public float speed;
    public float jumpForce;
    public LayerMask ground;
    public Collider2D coll;
    public Collider2D disColl;
    public int cherry = 0;
    public Text cherryNumber;
    private bool isHurt = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();

    }
    private void Update()
    {
        Jump();
        Crouch();
        cherryNumber.text = cherry.ToString();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHurt)
        {
            Movement();
        }
        SwitchAnime();
    }
    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float faceDirection = Input.GetAxisRaw("Horizontal");
        //move
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.fixedDeltaTime, rb.velocity.y);
            anime.SetFloat("Running", Mathf.Abs(faceDirection));
        }
        if(faceDirection != 0)
        {
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }
    }
    //切换动画效果
    void SwitchAnime()
    {
//            anime.SetBool("Idle", false);
            if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
            {
                anime.SetBool("Falling", true);
            }
            if (anime.GetBool("Jumping"))
            {
                if (rb.velocity.y < 0)
                {
                    anime.SetBool("Jumping", false);
                    anime.SetBool("Falling", true);
                }
                
            }
        if (isHurt)
        {
            anime.SetBool("Hurt", true);
            if (Mathf.Abs(rb.velocity.x) < 0.1)
            {
                anime.SetBool("Hurt", false);
 //               anime.SetBool("Idle", true);
                anime.SetFloat("Running", 0);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anime.SetBool("Falling", false);
 //           anime.SetBool("Idle", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收集
        if (collision.tag=="Collection")
        {
            Sound_Manager.instance.CherryAudio();
            //            cherryAudio.Play();
            //           Destroy(collision.gameObject);
            //           cherry += 1;
            collision.GetComponent<Animator>().Play("Get");
           
        }
        // 掉落
        if (collision.tag == "Deadline")
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 2f); 
        }
    }

    //消灭怪物
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anime.GetBool("Falling"))
            {

                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
                anime.SetBool("Jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-4, rb.velocity.y);
                Sound_Manager.instance.HurtAudio();
                //            hurtAudio.Play();
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(4, rb.velocity.y);
                Sound_Manager.instance.HurtAudio();
 //               hurtAudio.Play();
                isHurt = true;
            }

        }
    }

    void Crouch()
    {
        if (!Physics2D.OverlapCircle(cellingCheck.position,0.2f,ground))
        {
             if (Input.GetButton("Crouch"))
                    {
                        anime.SetBool("Crouching", true);
                        disColl.enabled = false;
                    }else{
                        anime.SetBool("Crouching", false);
                        disColl.enabled = true;
                    }
        }           
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            //         jumpAudio.Play();
            Sound_Manager.instance.JumpAudio();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            anime.SetBool("Jumping", true);
        }
    }

    public void CherryCount()
    {
        cherry = cherry + 1;
    }
}
