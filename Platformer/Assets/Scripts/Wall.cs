using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;

public class Wall : PCNode
{
    private Rigidbody2D rigid;
    private PlayerControl con;
    private Animator animator;
    public override void Init(GameObject obj)
    {
        rigid = obj.GetComponent<Rigidbody2D>();
        con = obj.GetComponent<PlayerControl>();
        animator = obj.GetComponent<Animator>();
    }
    public override void OnEnter()
    {
        rigid.rotation = 90;
        animator.SetTrigger("Hit");
    }
    public override void OnUpdate()
    {
        rigid.velocity = Vector2.zero;
    }
    public override void OnExit()
    {
        con.SetBool("Wall", false);
    }
}
