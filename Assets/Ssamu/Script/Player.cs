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
    
    public Scanner scanner;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();    
        anim = GetComponent<Animator>();    
        scanner = GetComponent<Scanner>();

    }

    

    void FixedUpdate() {
        if(!GameManager.instance.isLive){
            return;
        }

        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);   

        anim.SetFloat("RunState", inputVec.x != 0 || inputVec.y !=0 ? 0.5f : 0);
    }

    void OnMove(InputValue value){
        if(!GameManager.instance.isLive){
            return;
        }
        inputVec = value.Get<Vector2>();
    }


    private void HandleAttack() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            anim.SetTrigger("Attack");
        }
    }

     void LateUpdate() {
        if(!GameManager.instance.isLive){
            return;
        }
       
        if(inputVec.x > 0){
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }else if(inputVec.x < 0) {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }    
    }

    void OnCollisionStay2D(Collider2D collision){
        if(!GameManager.instance.isLive){
            return;
        }

        GameManager.instance.health -= Time.deltaTime * 10;

        if(GameManager.instance.health < 0){
            for(int index=2; index < transform.childCount; index++){
                transform.GetChild(index).gameObject.SetActive(false);
            }

            //Dead

        }
    }


}
