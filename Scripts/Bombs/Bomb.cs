using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float delay = 3f;
    private float countdown;
    private bool start;
    [SerializeField]
    private float speed;
    private Animator anim;
    private Rigidbody2D rb;
    public GameObject explosionEffect;
    void Start()
    {
        start = true;
        countdown = delay;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    
    void Update()
    {
        if (start)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                Explode();                
                start = false;
            }            
        }
    }

    void Explode()
    {
        if(explosionEffect != null)
        {
            GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(effect, .5f);
            Destroy(gameObject);
        }        
    }
}
