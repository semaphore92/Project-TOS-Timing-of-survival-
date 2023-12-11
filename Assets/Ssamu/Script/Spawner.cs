using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform[] spawnPoint;

    public SpawnData[] spawnData;

    int level;

    float timer;

    void Awake() {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {

        if(!GameManager.instance.isLive){
            return;
        }    

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f + 1), spawnData.Length - 1);

        if(timer > spawnData[level].spawnTime){
            timer = 0;
            Spawn();
        }
    }

    void Spawn(){
         GameObject enemy = GameManager.instance.pool.Get(level);
        
         enemy.transform.position = spawnPoint[UnityEngine.Random.Range(1,spawnPoint.Length)].position;
         Enemy enemyComponent = enemy.GetComponentInChildren<Enemy>();
         enemyComponent.Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData{
    public float spawnTime;
    public int health;
    public float speed;
}
