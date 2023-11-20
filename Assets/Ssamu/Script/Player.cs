using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float jumpPower;
    public bool isGrounded;

    Vector3 moveVector;

    Rigidbody2D rigid;

    Animator anim;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();    
        anim = GetComponent<Animator>();    

    }

    void Update(){
        HandleMovement();
        HandleJump();
        HandleAttack();
    }

    private void HandleMovement() {
        float moveX = Input.GetAxisRaw("Horizontal");
        moveVector = new Vector3(moveX, 0f, 0f);
        transform.Translate(moveVector.normalized * Time.deltaTime * speed);

        anim.SetFloat("RunState", moveX != 0 ? 0.5f : 0);
    }

    private void HandleJump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {        
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void HandleAttack() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            anim.SetTrigger("Attack");
        }
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

     void LateUpdate() {
       
        if(moveVector.x > 0){
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }else if(moveVector.x < 0) {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }    
    }



}
