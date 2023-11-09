using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    public Vector2 inputVec;

    public float speed;
    public float jumpPower;

    Rigidbody2D rigid;

    SpriteRenderer spriter;

    Animator anim;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();    
        spriter = GetComponent<SpriteRenderer>();  
        anim = GetComponent<Animator>();    

    }

    void Update() {
        if(Input.GetButtonDown("Jump")){
            Debug.Log("jump!!");
            rigid.AddForce(new Vector2(0,10), ForceMode2D.Impulse);
        }

        inputVec.x = Input.GetAxisRaw("Horizontal");    
    }

    void FixedUpdate() {
       // Vector2 nextVec = new Vector2(inputVec.x,0).normalized * speed * Time.fixedDeltaTime;
       // rigid.MovePosition(rigid.position + nextVec);    
    }


     void LateUpdate() {
        anim.SetFloat("Speed", Mathf.Abs(inputVec.x));

        if(inputVec.x != 0){
            spriter.flipX = inputVec.x < 0 ;
        }    
    }



}
