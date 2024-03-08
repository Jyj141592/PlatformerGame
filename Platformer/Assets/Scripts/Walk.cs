using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;

public class Walk : PCNode
{
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private PlayerControl con;
    private int rightIdx;
    private int directionIdx;
    private int direction = -1;
    [SerializeField]
    private float speed = 8.0f;
    public override void Init(GameObject obj)
    {
        rigid = obj.GetComponent<Rigidbody2D>();
        sprite = obj.GetComponent<SpriteRenderer>();
        con = obj.GetComponent<PlayerControl>();
        rightIdx = con.GetIndexOfParameter("Right");
        directionIdx = con.GetIndexOfParameter("Direction");
    }
    public override void OnUpdate()
    {
        if(con.GetBool(rightIdx)){
            direction = 1;
            sprite.flipX = true;
        }
        else{
            direction = -1;
            sprite.flipX = false;
        }
        con.SetInt(directionIdx, direction);
        rigid.velocity = new Vector2(speed * direction, rigid.velocity.y);   
    }

}
