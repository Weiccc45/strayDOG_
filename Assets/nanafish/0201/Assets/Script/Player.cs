using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("玩家移動速度")]
    public float Speed;
    [Header("玩家重力")]
    public Rigidbody2D rb;
    [Header("往上跳的力量")]
    public float jumpAmount = 35;

    void Start()
    {

    }

    void Update()
    {
        //控制玩家位移(上下左右)
        transform.Translate(Input.GetAxis("Horizontal")) * Speed, Input.GetAxis("Vertical") * Speed, 0);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //rb.AddForce(x,y),Vector2.up=(0,1),Vector2.down=(0,-1),Vector.left(-1,0)...
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D, Impulse);
        }
        if(rb.velocity.y >= 0)
        {
            rb.gravityScale = gravityScale;
        }
    }
}
