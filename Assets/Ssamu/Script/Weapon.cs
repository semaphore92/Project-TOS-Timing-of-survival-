using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;

    Player player;

    void Awake() {
        player = GameManager.instance.player;
    }

    void Update(){
        switch(id){

            case 0 :{
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                break;
            }

            default : {
                timer += Time.deltaTime;

                if(timer>speed){
                    timer = 0f;
                    Fire();
                }
                break;
            }
        }

        if(Input.GetButtonDown("Jump")){

            LevelUp(20,5);
        }
        
    }

    public void LevelUp(float damage, int count){
        this.damage = damage;
        this.count += count;

        if(id == 0){
            Batch();
        }

        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data){

        //Basic Set
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for(int index = 0; index < GameManager.instance.wPool.prefabs.Length; index++){
            if(data.projectile == GameManager.instance.wPool.prefabs[index]){
                prefabId = index;
                break;
            }
        }

        switch(id){

            case 0 :{
                speed = -150;
                Batch();
                break;
            }

            default:{
                speed = 0.3f;
                break;
            }
        }

        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
    }

    public void Batch(){

        for(int index = 0; index < count; index++){
            Transform bullet;

            if(index > transform.childCount){
                bullet =transform.GetChild(index);
            }else{
                bullet = GameManager.instance.wPool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index/count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage,-1,Vector3.zero);

        }
    }

    void Fire(){
        if(!player.scanner.nearestTarget){
            return;
        }

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.wPool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage,count,dir);

    }

}
