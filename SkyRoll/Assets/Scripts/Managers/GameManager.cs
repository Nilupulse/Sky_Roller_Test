using System.Collections;
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
    public bool ClearData;
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
        STARTED,
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
        if (ClearData) 
        {
            PlayerPrefs.DeleteAll();
        }
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
                gameStatus = GameStatus.STANDBY;
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
            progressNo = 0;
            gameStatus= GameStatus.STARTED;
            UIHandler.Instance.DemoComplete();
        }
        
    }
    
    public IEnumerator HatPowerUp() 
    {
        canPlay = false;
        PlayerMovements.Instance.powerUpMode = true;
        AnimationManager.Instance.PlayCamAnimation();
        yield return new WaitForSeconds(3f);
        AnimationManager.Instance.HatPowerUp();
        yield return new WaitForSeconds(3f);
        AnimationManager.Instance.ActivateWinCharacterAnimation(false);
        yield return new WaitForSeconds(3f);
        AnimationManager.Instance.ResetCamAnimation();
        PlayerMovements.Instance.PropellerHat();
        yield return new WaitForSeconds(2f);
        canPlay = true;
    }
    public void LevelCompleted() //when level completed
    {

        progressNo++;
        DataHandler.Instance.SaveData("ProgressNo", progressNo);
        gameStatus = GameStatus.WIN;
        PlayerMovements.Instance.SlowDownPlayer();
        currentLevelData.isCompleted = true;
        AnimationManager.Instance.PlayCamAnimation();
        AnimationManager.Instance.ActivateWinCharacterAnimation(true);
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
        AnimationManager.Instance.ResetCharacter();
        SetCurrentLevel();
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
