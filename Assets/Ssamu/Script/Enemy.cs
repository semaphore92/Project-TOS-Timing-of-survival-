using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{

    public float speed;
    public float health;
    public float maxHealth;

    bool isLive;

    bool hit;
    public Rigidbody2D target;

    Collider2D coll;
    Rigidbody2D rigid;

    Animator anim;

    WaitForFixedUpdate wait;

    SortingGroup sort;



    void Awake(){
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
        wait = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();
        sort = GetComponentInChildren<SortingGroup>();
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

        if(!isLive || hit){
            hit = false;
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
        coll.enabled = true;
        rigid.simulated = true;
        sort.sortingOrder = 2;
        //anim.SetTrigger("Dead");
        health = maxHealth;
    }

    public void Init(SpawnData data){
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

     void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.CompareTag("Bullet") || !isLive){
            return;
        }

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if(health > 0){
            hit = true;
        }else{
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            sort.sortingOrder = 1;
            anim.SetTrigger("Die");
            StartCoroutine(Dead());
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack(){
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    IEnumerator Dead(){
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        GameObject obj = transform.parent.gameObject; 
        obj.SetActive(false);
    }



}
