using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    private int player;
    public GameObject cam;
    private void Start() {
        player = LayerMask.NameToLayer("Player");
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == player){
            cam.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == player){
            cam.SetActive(false);
        }
    }
}
