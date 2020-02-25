using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [SerializeField]
    private float movSpeed = 8f;
    [SerializeField]
    private float jumpForce = 500f;
    [SerializeField]
    private float attackDelay = .5f;
    [SerializeField]
    private Transform groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    Rigidbody2D rb;
    Animator anim;
    private bool facingRight;
    private bool isGrounded;
    private bool jump;
    
    void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        facingRight = true;    
    }

    void FixedUpdate()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoints.position, groundRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }
        float movX = Input.GetAxisRaw("Horizontal2");                
        HandleMovement(movX);
        ResetValues();        
    }

    void HandleMovement(float horizontal)
    {
        rb.velocity = new Vector2(horizontal * movSpeed, rb.velocity.y);
        anim.SetFloat("move", Mathf.Abs(horizontal));        
        if(horizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if(horizontal < 0 && facingRight)
        {
            Flip();
        }
        if(Input.GetButtonDown("Jump2"))
        {
            jump = true;
            if(isGrounded && jump)
            {
                isGrounded = false;
                rb.AddForce(new Vector2(0f, jumpForce));
            }
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(AttackCo());
        }
    }

    IEnumerator AttackCo()
    {                
        anim.SetBool("attack", true);        
        yield return new WaitForSeconds(attackDelay);
        anim.SetBool("attack", false);
    }

    void ResetValues()
    {
        jump = false;
    }

    void Flip()
    {        
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
