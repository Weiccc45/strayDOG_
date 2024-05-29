using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkctrl : MonoBehaviour
{

    public float speed = 5f; // 移动速度
    public float jumpForce = 5f; // 跳跃力
    public float groundCheckRadius = 0.1f; // 地面检查的半径
    public float maxFallSpeed = -10f; 
    public float minY = 0f; // 最低高度
    public float maxY = 10f; // 最大高度
    public float verticalSpeed = 5f;
    public float runSpeed = 10f; // 跑步速度
    private bool isWalking; // 是否在行走
    private bool facingRight = true; // 是否面向右侧
    private bool isGrounded; // 是否在地面上
    private bool hasJumped = false;
    private bool isRunning = false; 
    private Animator animator; // 动画控制器
    private Rigidbody2D rb; // 2D刚体组件
    public LayerMask whatIsGround; // 用于检测地面的Layer
    public Transform groundCheck; // 地面检查的Transform
    

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        float moveX = 0f;
    isWalking = false; // 每次更新时重置为false

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

    if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !hasJumped) // 检查跳跃输入，并确保在地面上
    {
        Debug.Log("Jumping!");
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isGrounded = false;
        animator.SetBool("IsJumping", true); // 设置 IsJumping 为 true，用于触发跳跃动画
        hasJumped = true; // 标记为已经跳跃过
    }

    if (Input.GetKeyDown(KeyCode.LeftShift))
    {
        isRunning = true;
    }
    else if (Input.GetKeyUp(KeyCode.LeftShift))
    {
        isRunning = false;
    }

    // 移动角色
    transform.Translate(Vector3.right * moveX);
    float verticalInput = Input.GetAxis("Vertical");
    transform.Translate(Vector3.up * verticalInput * (isRunning ? runSpeed : speed) * Time.deltaTime);

    animator.SetBool("isWalking", isWalking); // 设置动画参数
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果玩家与地面碰撞，则可以再次跳跃
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasJumped = false;
            animator.SetBool("IsJumping", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // 玩家离开地面时将 isGrounded 设为 false
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
        scale.x *= -1; // 反转x轴方向的缩放
        transform.localScale = scale;
    }

    void FixedUpdate()
    {
        // 检查是否在地面上
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (isGrounded)
        {
            // 在地面上，不进行垂直速度的修改
        }
        else
        {
            // 不在地面上，可以进行下落速度的限制
            if (rb.velocity.y < maxFallSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
            }
        }
    }

    
}
