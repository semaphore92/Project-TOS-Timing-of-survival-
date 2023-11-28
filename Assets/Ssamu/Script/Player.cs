using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{

    public float speed;

    public Vector2 inputVec;

    Rigidbody2D rigid;

    Animator anim;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();    
        anim = GetComponent<Animator>();    

    }

    void FixedUpdate() {

        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);   

        anim.SetFloat("RunState", inputVec.x != 0 || inputVec.y !=0 ? 0.5f : 0);


    }

    void OnMove(InputValue value){
        inputVec = value.Get<Vector2>();
    }


    private void HandleAttack() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            anim.SetTrigger("Attack");
        }
    }

     void LateUpdate() {
       
        if(inputVec.x > 0){
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }else if(inputVec.x < 0) {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }    
    }



}
