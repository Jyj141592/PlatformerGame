using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator animator;
    public BoxCollider2D col;
    public SpriteRenderer sprite;
    public int direction = -1;

    public float speed = 5.0f;
    public float power = 8.0f;
    private float pressTime = 0.0f;

    public bool isAir{
        get => Physics2D.OverlapCircle(transform.position, 0.05f, groundLM) == null;
    }

    int groundLayer;
    int groundLM;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        groundLayer = LayerMask.NameToLayer("Ground");
        groundLM = 1 << groundLayer;
    }

    void Update()
    {
        if(!isAir){
            if(Input.GetKey(KeyCode.LeftArrow)){
                if(direction == 1){
                    direction = -1;
                    sprite.flipX = false;
                }
                rigid.velocity = new Vector2(speed * direction, rigid.velocity.y);
            }
            else if(Input.GetKey(KeyCode.RightArrow)){
                if(direction == -1){
                    direction = 1;
                    sprite.flipX = true;
                }
                rigid.velocity = new Vector2(speed * direction, rigid.velocity.y);
            }
            else if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)){
                rigid.velocity = Vector2.zero;
            }
            else if(Input.GetKeyDown(KeyCode.Space)){
                rigid.velocity = Vector2.zero;
                pressTime = Time.time;
            }
            else if(Input.GetKeyUp(KeyCode.Space)){
                //Debug.Log((Time.time - pressTime).ToString() + " Jump!");
                float time = Time.time - pressTime;
                if(time > 2) time = 2.0f;
                float angle = Mathf.PI * time / 4;
                Vector2 jumpDir = new Vector2(Mathf.Cos(angle) * power * direction, Mathf.Sin(angle) * power);
                rigid.velocity = jumpDir;
            }
        }
    }
}
