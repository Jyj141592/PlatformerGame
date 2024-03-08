using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;
using PlayerController.Editor;

public class Idle : PCNode
{
    private Rigidbody2D rigid;
    public override void Init(GameObject obj)
    {
        rigid = obj.GetComponent<Rigidbody2D>();
    }
    public override void OnEnter()
    {
        rigid.velocity = Vector2.zero;
    }
}
