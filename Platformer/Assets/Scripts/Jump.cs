using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;
using UnityEditor.Experimental.GraphView;

public class Jump : PCNode
{
    private Rigidbody2D rigid;
    private Animator animator;
    private PlayerControl con;
    private BoxCollider2D bCol;
    private CircleCollider2D cCol;
    private int speedHash;


    public override void Init(GameObject obj)
    {
        rigid = obj.GetComponent<Rigidbody2D>();
        animator = obj.GetComponent<Animator>();
        con = obj.GetComponent<PlayerControl>();
        bCol = obj.GetComponent<BoxCollider2D>();
        cCol = obj.GetComponent<CircleCollider2D>();
        speedHash = Animator.StringToHash("Speed");
    }
    public override void OnEnter()
    {
        con.SetBool("Ground", false);
        bCol.enabled = false;
        cCol.enabled = true;
        animator.SetBool("IsAir", true);
    }
    public override void OnUpdate()
    {
        float speed = rigid.velocity.sqrMagnitude;
        animator.SetFloat(speedHash, speed);
        int direction;
        if(rigid.velocity.x > 0) direction = 1;
        else direction = -1;
        Vector2 v = rigid.velocity.normalized;

        float a = Mathf.Acos(v.y) * 180 * direction * -1 / Mathf.PI;
        rigid.rotation = a;
    }
    public override void OnExit()
    {
        cCol.enabled = false;
        bCol.enabled = true;
    }
}
