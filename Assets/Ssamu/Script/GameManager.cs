using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isLive;
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
    public LevelUp uiLevelUp;

    void Start() {
        health = maxHealth;    

        //임시 스크립트
        uiLevelUp.Select(0);
    }

    void Awake() {
        instance = this;    
    }

    void Update() {

        if(!isLive){
            return;
        }
        
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime){
            gameTime = maxGameTime;
        }

        
    }

    public void GetExp(){
        exp++;

        if(exp == nextExp[Mathf.Min(level,nextExp.Length-1)]){
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop(){
        isLive = false;
        Time.timeScale = 0;
    
    }

    public void Resume(){
        isLive = true;
        Time.timeScale = 1; 
    
    }
}
