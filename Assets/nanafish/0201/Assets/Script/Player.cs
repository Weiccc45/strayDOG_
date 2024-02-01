using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("���a���ʳt��")]
    public float Speed;
    [Header("���a���O")]
    public Rigidbody2D rb;
    [Header("���W�����O�q")]
    public float jumpAmount = 35;

    void Start()
    {

    }

    void Update()
    {
        //����a�첾(�W�U���k)
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
