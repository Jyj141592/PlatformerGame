using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;
[RequireComponent(typeof(PlayerControl))]
public class PlayerInput : MonoBehaviour
{
    private PlayerControl con;
    void Start()
    {
        con = GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            con.SetBool("Left", true);
        }
        else if(Input.GetKeyUp(KeyCode.LeftArrow)){
            con.SetBool("Left", false);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow)){
            con.SetBool("Right", true);
        }
        else if(Input.GetKeyUp(KeyCode.RightArrow)){
            con.SetBool("Right", false);
        }
        else if(Input.GetKeyDown(KeyCode.Space)){
            con.SetBool("Jump", true);
        }
        else if(Input.GetKeyUp(KeyCode.Space)){
            con.SetBool("Jump", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        var p = other.GetContact(0).point;
        Vector2 v = new Vector2(p.x - transform.position.x, p.y - transform.position.y);
        v.Normalize();

        if(v.y < 0 && -v.y > 0.8f){
            con.SetBool("Ground", true);
        }
        else if(v.y > 0 && v.y > 0.7071f) return;
        else {
            con.SetBool("Wall", true);
        }
    }
}
