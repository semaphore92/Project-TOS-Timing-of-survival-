using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    SpriteRenderer[] spriters;

    void Awake(){
        rigid = GetComponent<Rigidbody2D>();
        spriters = GetComponentsInChildren<SpriteRenderer>();  
    }

    void FixedUpdate() {
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;


    }

    void LateUpdate(){
        if(target.position.x < rigid.position.x){
             transform.localScale = new Vector3(1,transform.localScale.y, transform.localScale.z);
        }else if(target.position.x > rigid.position.x){
             transform.localScale = new Vector3(-1,transform.localScale.y, transform.localScale.z);
        }
    }
}
