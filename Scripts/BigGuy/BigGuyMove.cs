using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGuyMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 8f;
    [SerializeField]
    private float jumpForce = 500f;
    [SerializeField]
    private float attackDelay = .5f;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private Transform groundPoint;
    private bool isGrounded;
    private bool jump;
    private bool facingRight;
    private bool isAttacking;
    Rigidbody2D rb;
    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        facingRight = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoint.position, groundRadius, whatIsGround);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }
        float movX = Input.GetAxisRaw("Horizontal");
        HandleMovement(movX);
        ResetValues();
    }

    void HandleMovement(float horizontal)
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        anim.SetFloat("move", Mathf.Abs(horizontal));
        if(horizontal > 0 && !facingRight)
            Flip();
        else if(horizontal < 0 && facingRight)
            Flip();
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            if(isGrounded && jump)
            {
                isGrounded = false;
                rb.AddForce(new Vector2(0, jumpForce));
            }
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            isAttacking = true;
            if(isAttacking)
            {
                StartCoroutine(AttackCo());
            }
        }
    }

    IEnumerator AttackCo()
    {
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(.5f);
        anim.SetBool("attack", false);
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
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
