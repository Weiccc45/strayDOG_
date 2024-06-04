using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkctrl : MonoBehaviour
{
    
    public float speed = 5f; 
    public float jumpForce = 5f;
    public float groundCheckRadius = 0.1f; 
    public float maxFallSpeed = -10f; 
    public float minY = 0f; 
    public float maxY = 10f;
    public float verticalSpeed = 5f;
    public float runSpeed = 10f; 
    private bool isWalking;
    private bool facingRight = true; 
    private bool isGrounded;
    private bool hasJumped = false;
    private bool isRunning = false;
    private bool isAttacking = false; 
    private Animator animator; 
    private Rigidbody2D rb;
    public LayerMask whatIsGround; 
    public Transform groundCheck;
    public GameObject backpack;
    bool isOpen;



    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    void Update() 
    {
        Openbackpack();

        float moveX = 0f;
        isWalking = false;

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
            isWalking = true;
            if (facingRight)
            {
                Flip();
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
            isWalking = true;
            if (!facingRight)
            {
                Flip();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        float currentSpeed = isRunning ? runSpeed : speed;
        transform.Translate(Vector3.right * moveX * currentSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !hasJumped)
        {
            Debug.Log("Jumping!");
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("IsJumping", true);
            hasJumped = true;
        }

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);

        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            Debug.Log("Attacking!");
            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasJumped = false;
            animator.SetBool("IsJumping", false);
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (isGrounded)
        {
            // 在地面上，不进行垂直速度的修改
        }
        else
        {
            if (rb.velocity.y < maxFallSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
            }
        }
    }


    public void ResetAttack()
    {
        Debug.Log("Resetting Attack");
        isAttacking = false;
    }

    void Openbackpack()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpen = !isOpen;
            backpack.SetActive(isOpen);
        }
    }


}
