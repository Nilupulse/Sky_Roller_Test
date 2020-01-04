﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton class
    public static GameManager Instance;
    //Public variables
    public bool canPlay;
    public bool standby;
    public bool isFirstTime;
    public int progressNo;
    public int playerGemCount;
    public float boostTime;
    public List<LevelData> levelData = new List<LevelData>();
    public List<GameObject> levels = new List<GameObject>();
    public GameObject hitObject;
    //Private variables
    LevelData currentLevelData;
    GameObject currentLevel;
    //Game status 
    public enum GameStatus 
    {
        STANDBY,
        PLAYING,
        REVIVE,
        WIN,
        HIT,
        GAMEOVER
    }
    public GameStatus gameStatus;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    void Start()
    {
       //PlayerPrefs.DeleteAll();
        Initialize();
    }
    void Initialize() 
    {
        canPlay = false;
        //Retrive data before stating
        progressNo = DataHandler.Instance.GetIntData("ProgressNo");
        playerGemCount = DataHandler.Instance.GetIntData("GemCount");
        isFirstTime = DataHandler.Instance.GetPlayerStatus("IsFirstTime");
        boostTime = 3f;
        SetCurrentLevel();
        UIHandler.Instance.StartGame();
    }
    public void SetCurrentLevel() //Setup the level
    {
        isFirstTime = DataHandler.Instance.GetPlayerStatus("IsFirstTime");
        if (progressNo <= 2)//to limit the demo
        {
            if (isFirstTime)
            {
                currentLevelData = levelData[progressNo];
                currentLevel = Instantiate(levels[progressNo]) as GameObject;
            }
            else
            {
                currentLevelData = null;
                if (currentLevel !=null) //when close and open the game check current level
                {
                    Destroy(currentLevel);
                }
                currentLevelData = levelData[progressNo];
                currentLevel = Instantiate(levels[progressNo]) as GameObject;
                
            }
            UIHandler.Instance.SetGameInfo(currentLevelData);
        }
        else//only for the demo
        {
            UIHandler.Instance.DemoComplete();
        }
        
    }   
    public void LevelCompleted() //when level completed
    {

        progressNo++;
        DataHandler.Instance.SaveData("ProgressNo", progressNo);
        gameStatus = GameStatus.WIN;
        PlayerMovements.Instance.SlowDownPlayer();
        currentLevelData.isCompleted = true;
        UIHandler.Instance.PlayCamAnimation();
        UIHandler.Instance.ActivateCompletedLevel();
        if (currentLevelData.levelID == 4)
        {
            UIHandler.Instance.ResetActivateCompletedLevel();
        }        

    }
    public void GameOver()//when collide with an obstical
    {
        canPlay = false;
        UIHandler.Instance.GameOverUI();
    }
    public void SetHitObject(GameObject _hitObject) //set the hit object
    {
        hitObject=null;
        hitObject = _hitObject;
    }
    public void ReviveGame() //revive game
    {
        Destroy(hitObject);
        gameStatus = GameStatus.STANDBY;
    }
    public void LoadLevel() //load level(same or next)
    {
        PlayerMovements.Instance.ResetPlayer();
        SetCurrentLevel();
        gameStatus = GameStatus.STANDBY;
    }
    public void ResetGame() //to reset the game after the demo
    {
        float tempPlayerSpeed= DataHandler.Instance.GetFloatData("PlayerSpeed");
        PlayerPrefs.DeleteAll();
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        foreach (LevelData obj in levelData) 
        {
            obj.isCompleted = false;
        }
        DataHandler.Instance.SaveData("PlayerSpeed", tempPlayerSpeed);
        Initialize();
    }
}
