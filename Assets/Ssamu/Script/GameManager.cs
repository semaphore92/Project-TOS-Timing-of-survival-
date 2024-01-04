using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Player Info")]
    public float health;
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
    public Result uiResult;
    public GameObject enemyCleaner;

    public void GameStart() {
        health = maxHealth;    

        uiLevelUp.Select(0); //임시 스크립트
        isLive = true;
        Resume();
    }

    public void GameRetry(){
        SceneManager.LoadScene(0);
    }

    public void GameOver(){
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine(){
        isLive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }

    public void GameVictory(){
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine(){
        isLive = false;
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
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
            GameVictory();
        }

        
    }

    public void GetExp(){
        if(isLive){
            return;
        }
        
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
