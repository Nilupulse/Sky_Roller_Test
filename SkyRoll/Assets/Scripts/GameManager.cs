using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool canPlay;
    public bool standby;
    public bool isFirstTime;
    public int playerScore;
    public int progressNo;
    public float boostTime;
    public List<LevelData> levelData = new List<LevelData>();
    public List<GameObject> levels = new List<GameObject>();
    public GameObject hitObject;
    
    LevelData currentLevelData;
    GameObject currentLevel;

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
        canPlay = false;
        playerScore = 0;
        progressNo = 0;
        boostTime = 3f;
        SetCurrentLevel();
        UIHandler.Instance.StartGame();
    }
    public void SetCurrentLevel() 
    {
        print("progressNo1"+ progressNo);
        if (isFirstTime)
        {
            currentLevelData = levelData[progressNo];
            currentLevel= Instantiate(levels[progressNo]) as GameObject;
        }
        else
        {
            currentLevelData = null;
            Destroy(currentLevel);
            currentLevelData = levelData[progressNo];
            currentLevel = Instantiate(levels[progressNo]) as GameObject;
        }
    }
    public void GameOver()
    {
        canPlay = false;
        UIHandler.Instance.GameOverUI();
    }
    public void LevelCompleted() 
    {
        progressNo++;
        gameStatus = GameStatus.WIN;
        PlayerMovements.Instance.SlowDownPlayer();
        currentLevelData.isCompleted = true;
        UIHandler.Instance.GameWin(currentLevelData.levelName);
        UIHandler.Instance.ActivateCompletedLevel(currentLevelData.levelID);
        if (currentLevelData.levelID == 4)
        {
            UIHandler.Instance.ResetActivateCompletedLevel();
        }

    }
    public void SetHitObject(GameObject _hitObject) 
    {
        hitObject=null;
        hitObject = _hitObject;
    }
    public void ReviveGame() 
    {
        Destroy(hitObject);
        gameStatus = GameStatus.STANDBY;
    }
    public void LoadLevel() 
    {
        PlayerMovements.Instance.ResetPlayer();
        SetCurrentLevel();
        gameStatus = GameStatus.STANDBY;
    }
}
