using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anime;
    protected AudioSource deathAudio;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        anime = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Death()
    {

        Destroy(gameObject);
    }
    public void JumpOn()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
//        deathAudio.Play();
        anime.SetTrigger("Death");
    }
}
