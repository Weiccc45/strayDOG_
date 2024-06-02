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
    

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        
        float moveX = 0f;
        isWalking = false; 

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -speed * Time.deltaTime;
            isWalking = true;
            if (facingRight)
            {
                Flip();
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveX = speed * Time.deltaTime;
            isWalking = true;
            if (!facingRight)
            {
                Flip();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !hasJumped) 
        {
            Debug.Log("Jumping!");
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("IsJumping", true); 
            hasJumped = true; 
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        transform.Translate(Vector3.right * moveX);
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * verticalInput * (isRunning ? runSpeed : speed) * Time.deltaTime);

        animator.SetBool("isWalking", isWalking);

        if (Input.GetMouseButtonDown(0) && !isAttacking) // 按下左键进行攻击
        {
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
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
        }
    }
    
    void Jump()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        hasJumped = true;
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
        isAttacking = false;
    }
    
}
