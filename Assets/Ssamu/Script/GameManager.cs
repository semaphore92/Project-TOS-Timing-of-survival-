using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Player Info")]
    public int health;
    public int maxHealth=100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = {10,30,60,100,150,210,280,360,450,600};

    [Header("# Game Object")]
    public PoolManager pool;
    public WpoolManager wPool;
    public Player player;

    void Start() {
        health = maxHealth;    
    }

    void Awake() {
        instance = this;    
    }

    void Update() {

        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime){
            gameTime = maxGameTime;
        }

        
    }

    public void GetExp(){
        exp++;

        if(exp == nextExp[level]){
            level++;
            exp = 0;
        }
    }
}
