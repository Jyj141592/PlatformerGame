using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;
using Unity.Mathematics;

public class Ground : PCNode
{
    private Rigidbody2D rigid;
    private Animator animator;
    public override void Init(GameObject obj)
    {
        rigid = obj.GetComponent<Rigidbody2D>();
        animator = obj.GetComponent<Animator>();
    }
    public override void OnEnter()
    {
        rigid.velocity = Vector2.zero;
        rigid.transform.rotation = quaternion.identity;
        animator.SetBool("IsAir", false);
        animator.SetTrigger("Hit");
    }
}
