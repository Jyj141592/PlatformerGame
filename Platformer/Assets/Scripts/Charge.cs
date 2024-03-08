using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;

public class Charge : PCNode
{
    private Rigidbody2D rigid;
    private PlayerControl con;
    private Animator animator;
    private int chargeHash;
    private float pushTime;
    private int angleIdx;
    [SerializeField]
    private float maxChargeTime = 1.5f;
    
    [SerializeField]
    private float jumpPower = 13.0f;
    public override void Init(GameObject obj)
    {
        rigid = obj.GetComponent<Rigidbody2D>();
        animator = obj.GetComponent<Animator>();
        chargeHash = Animator.StringToHash("Charge");
        con = obj.GetComponent<PlayerControl>();
        angleIdx = con.GetIndexOfParameter("Angle");
    }
    public override void OnEnter()
    {
        rigid.velocity = Vector2.zero;
        pushTime = Time.time;
        animator.SetBool(chargeHash, true);
    }
    public override void OnUpdate()
    {
        if(Time.time - pushTime > maxChargeTime){
            con.SetBool("Jump", false);
        }
    }
    public override void OnExit()
    {
        float t = Time.time - pushTime;
        if(t > maxChargeTime) t = maxChargeTime;
        float angle = (Mathf.PI * t) / (2 * maxChargeTime);
        con.SetFloat(angleIdx, angle);
        con.SetBool("Land", false);
        animator.SetBool(chargeHash, false);
        int direction = con.GetInt("Direction");
        rigid.velocity = new Vector2(jumpPower * Mathf.Cos(angle) * direction, jumpPower * Mathf.Sin(angle));
    }
}
