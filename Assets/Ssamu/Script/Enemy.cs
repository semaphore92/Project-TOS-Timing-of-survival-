using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public float health;
    public float maxHealth;

    bool isLive;
    public Rigidbody2D target;

    Rigidbody2D rigid;

    Animator anim;

    void Awake(){
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
    }

    void FixedUpdate() {

        if(!isLive){
            return;
        }

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;

        anim.SetFloat("RunState", 0.5f );
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
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data){
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

     void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.CompareTag("Bullet")){
            return;
        }

        health -= collision.GetComponent<Bullet>().damage;

        if(health > 0){

        }else{
            Dead();
        }
    }

    void Dead(){
        gameObject.SetActive(false);
    }



}
