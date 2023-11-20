using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;

    public bool isGrounded;

    bool isLive = true;

    public Rigidbody2D target;


    Rigidbody2D rigid;

    void Awake(){
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {

        if(!isLive){
            return;
        }

        if(!isGrounded){
            return;
        }

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate(){

        if(!isLive){
            return;
        }

        if(target.position.x < rigid.position.x){
             transform.localScale = new Vector3(1,transform.localScale.y, transform.localScale.z);
        }else if(target.position.x > rigid.position.x){
             transform.localScale = new Vector3(-1,transform.localScale.y, transform.localScale.z);
        }
    }

    void OnEnable(){
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();  
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = false;
        }
    }
}
