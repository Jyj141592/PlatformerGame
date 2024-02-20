using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Components
    public Rigidbody2D rigid;
    public Animator animator;
    public BoxCollider2D bCol;
    public SpriteRenderer sprite;

    // States
    public int direction = -1;
    public float speed = 5.0f;
    public float power = 8.0f;
    public float holdTime = 1.5f;
    private float pressTime = 0.0f;

    // Conditions
    public bool isAir{
        get => Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 1), 0.05f, groundLM) == null;
    }
    private bool isCharge = false;

    // Layers
    int groundLayer;
    int groundLM;

    // Hashes
    int isAir_hash;
    int charge_hash;
    int move_hash;
    int speed_hash;
    int hit_hash;

    void Start()
    {
        // Initialize Components
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bCol = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        // Initialize Layers
        groundLayer = LayerMask.NameToLayer("Ground");
        groundLM = 1 << groundLayer;
        // Initialize Hashes
        isAir_hash = Animator.StringToHash("IsAir");
        charge_hash = Animator.StringToHash("Charge");
        move_hash = Animator.StringToHash("Move");
        speed_hash = Animator.StringToHash("Speed");
        hit_hash = Animator.StringToHash("Hit");
        Time.timeScale = 0.1f;
    }

    void Update()
    {
        if(!isAir){
            animator.SetBool(isAir_hash, false);
            if(Input.GetKey(KeyCode.LeftArrow)){
                animator.SetBool(move_hash, true);
                if(direction == 1){
                    direction = -1;
                    sprite.flipX = false;
                }
                rigid.velocity = new Vector2(speed * direction, rigid.velocity.y);
            }
            else if(Input.GetKey(KeyCode.RightArrow)){
                animator.SetBool(move_hash, true);
                if(direction == -1){
                    direction = 1;
                    sprite.flipX = true;
                }
                rigid.velocity = new Vector2(speed * direction, rigid.velocity.y);
            }
            else if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)){
                rigid.velocity = Vector2.zero;
                animator.SetBool(move_hash, false);
            }
            if(Input.GetKey(KeyCode.Space)){
                if(Input.GetKeyDown(KeyCode.Space)){
                    rigid.velocity = Vector2.zero;
                    pressTime = Time.time;
                    isCharge = true;
                    animator.SetBool(charge_hash, true);
                }
                if(Time.time - pressTime > holdTime){
                    Jump();
                }
            }
            else if(Input.GetKeyUp(KeyCode.Space) && isCharge){
                Jump();
            }
            
        }
        else{
            animator.SetBool(isAir_hash, true);
            animator.SetFloat(speed_hash, rigid.velocity.sqrMagnitude);
            Vector2 v = rigid.velocity;
            if(v == Vector2.zero) return;
            v.Normalize();
            float angle = Mathf.Acos(v.y);
            angle = angle * 180 * direction * -1 / Mathf.PI;
            //transform.rotation = Quaternion.Euler(0,0,angle);
            rigid.rotation = angle;
        }
    }

    private void Jump(){
        isCharge = false;
        animator.SetBool(charge_hash, false);
        float time = Time.time - pressTime;
        if(time > holdTime) time = holdTime;
        float angle = Mathf.PI * time / (2 * holdTime);
        Vector2 jumpDir = new Vector2(Mathf.Cos(angle) * power * direction, Mathf.Sin(angle) * power);
        rigid.velocity = jumpDir;

        animator.SetFloat(speed_hash, rigid.velocity.sqrMagnitude);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == groundLayer){
            animator.SetTrigger(hit_hash);
        }
    }
}
