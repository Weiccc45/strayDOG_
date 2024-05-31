using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float attackRange = 1f; // 攻击范围
    public LayerMask enemyLayer; // 敌人的Layer
    private Animator animator; // 动画控制器
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 检测左键按下
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        // 播放攻击动画
        animator.SetTrigger("Attack");
    }

 
}
