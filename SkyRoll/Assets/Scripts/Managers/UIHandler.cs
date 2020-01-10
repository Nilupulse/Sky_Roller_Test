using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    //Singleton class
    public static UIHandler Instance;
    //UI Screens
    public GameObject SplashScreen;
    public GameObject InfoScreen;
    public GameObject StartScreen;
    public GameObject GamePlayScreen;
    public GameObject GameWinScreen;
    public GameObject GameOverScreen;
    public GameObject SettingsScreen;
    public GameObject DemoCompleteScreen;

    public GameObject Confetti;
    public GameObject leftTrail;
    public GameObject rightTrail;
    public GameObject btnNoThanks;

    public Image[] completedLevels = new Image[4];
    public Image progressBar;

    public Text levelNameText;
    public Text gemCountText;
    public Text levelLoadingText;

    public Animator camAnim;
    public GameObject camera;
    public Button soundButton;
    public Slider gameProgress;

    public float chanceTime;
    bool secondChance;
    bool demoAgain;

    int levelID;
    int levelLenth;
    int levelGemCount;
    string levelName;
    Vector3 camPos;


    void Awake() 
    {
        if (!Instance) 
        {
            Instance = this;
        }
    }

    #region Game Start
    public void StartGame()
    {
        camPos = camera.transform.position;
        ResetActivateCompletedLevel();        
        StartCoroutine(ActivateUI());
        demoAgain = true;
    }
    IEnumerator ActivateUI() 
    {
        yield return new WaitForSeconds(5f);
        DissableScreens();
        if (GameManager.Instance.isFirstTime)
        {
            InfoScreen.SetActive(true);
            DataHandler.Instance.SaveData("IsFirstTime",false);
        }
        else
        {
            ActivateStartScreen();
        }
        
    }
    void ActivateStartScreen()
    {
        StartScreen.SetActive(true);
        gemCountText.text = "" + GameManager.Instance.playerGemCount;
        foreach (LevelData completedLevel in GameManager.Instance.levelData)
        {
            if (completedLevel.isCompleted)
            {
                completedLevels[completedLevel.levelID].enabled = true;
            }
        }
        SoundManager.Instance.SoundStatus();

    }
    public void SetGameInfo(LevelData currentLevelData)
    {
        levelID = 0;
        levelLenth = 0;
        levelGemCount = 0;
        levelName = "";

        levelID = currentLevelData.levelID;
        levelLenth = currentLevelData.levelLenth;
        levelGemCount = currentLevelData.gemCount;
        levelName = currentLevelData.levelName;

        gameProgress.minValue = 0;
        gameProgress.maxValue = levelLenth;
        levelNameText.text = "";
        levelNameText.text = levelName;

    }
    #endregion

    #region Game Play
    public void PlayGame()
    {
        DissableScreens();
        StartScreen.SetActive(true);
        SoundManager.Instance.SoundStatus();
    }
    #endregion

    #region Game Over
    public void GameOverUI() 
    {
        DissableScreens();
        GameOverScreen.SetActive(true);
        secondChance = true;
    }
    public void Revive()
    {
        GameManager.Instance.gameStatus = GameManager.GameStatus.REVIVE;
        ResetCircleProgressBar();
        GameManager.Instance.ReviveGame();
        DissableScreens();
        ActivateStartScreen();
    }
    public void GameOver()
    {
        GameManager.Instance.gameStatus = GameManager.GameStatus.GAMEOVER;
        DissableScreens();
        ResetCircleProgressBar();
        ActivateStartScreen();
        GameManager.Instance.LoadLevel();
    }
    void ResetCircleProgressBar()
    {
        secondChance = false;
        progressBar.fillAmount = 0;
        btnNoThanks.SetActive(false);
    }
    #endregion

    #region Game Win
    public void GameWin()
    {
        Confetti.SetActive(true);
        DissableScreens();
        GameWinScreen.transform.GetChild(1).GetComponent<Text>().text = levelName;
        GameWinScreen.SetActive(true);

    }
    public void ClaimDoubleGems()//inside this can play ads
    {
        GameManager.Instance.playerGemCount += levelGemCount * 2;
        DataHandler.Instance.SaveData("GemCount", GameManager.Instance.playerGemCount);
        GemClaimed();
    }
    public void ClaimGems()
    {
        GameManager.Instance.playerGemCount += levelGemCount;
        DataHandler.Instance.SaveData("GemCount", GameManager.Instance.playerGemCount);
        GemClaimed();
    }
    void GemClaimed()
    {
        DissableScreens();
        ActivateStartScreen();
        Confetti.SetActive(false);
        AnimationManager.Instance.ResetCamAnimation();
        GameManager.Instance.LoadLevel();
    }
    public void ActivateCompletedLevel()
    {
        completedLevels[levelID].enabled = true;
    }
    public void ResetActivateCompletedLevel()
    {
        foreach (Image completedLevel in completedLevels)
        {
            completedLevel.enabled = false;
        }
    }
    #endregion

    #region Demo
    public void DemoComplete()
    {
        DissableScreens();
        DemoCompleteScreen.SetActive(true);
    }
    public void PlayDemoAgain()
    {
        if (demoAgain) 
        {
            demoAgain = false;
            levelLoadingText.enabled = true;
            ResetActivateCompletedLevel();
            GameManager.Instance.ResetGame();
        }  

    }
    #endregion

    #region Settings
    public void OpenSettings()
    {
        SettingsScreen.SetActive(true);
    }
    public void CloseSettings()
    {
        SettingsScreen.SetActive(false);
    }
    public void SoundONOFF()
    {
        var colors = soundButton.colors;
        if (SoundManager.Instance.isSoundOn)
        {
            soundButton.GetComponentInChildren<Text>().text = "OFF";
            soundButton.colors = colors;
        }
        else
        {
            soundButton.GetComponentInChildren<Text>().text = "ON";
            soundButton.colors = colors;
        }
        SoundManager.Instance.SoundOption();
    }
    #endregion

    void DissableScreens() 
    {
        levelLoadingText.enabled = false;
        SplashScreen.SetActive(false);
        InfoScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        GamePlayScreen.SetActive(false);
        StartScreen.SetActive(false);
        GameWinScreen.SetActive(false);
        DemoCompleteScreen.SetActive(false);
        SettingsScreen.SetActive(false);
    } 
    void Update()
    {
        if (GameManager.Instance.gameStatus == GameManager.GameStatus.PLAYING)
        {
            DissableScreens();
            GamePlayScreen.SetActive(true);
            gameProgress.value = PlayerMovements.Instance.player.transform.position.z;
            leftTrail.SetActive(true);
            rightTrail.SetActive(true);
        }
        else 
        {
            leftTrail.SetActive(false);
            rightTrail.SetActive(false);
        }
        if (secondChance) 
        {
            progressBar.fillAmount += 1f / 10f * Time.deltaTime;
            if (progressBar.fillAmount == 1)
            {
                btnNoThanks.SetActive(true);
            }
        }
    }
}
