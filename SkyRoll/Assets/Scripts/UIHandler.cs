using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject SplashScreen;
    public GameObject InfoScreen;
    public GameObject StartScreen;
    public GameObject GamePlayScreen;
    public GameObject GameWinScreen;
    public GameObject GameOverScreen;
    public GameObject DemoCompleteScreen;
    public GameObject Confetti;
    public Image[] completedLevels = new Image[4];
    public Text levelNameText;
    public Text gemCountText;
    public Animator camAnim;
    public GameObject camera;

    public Image progressBar;
    public float chanceTime;
    public GameObject btnNoThanks;

    int levelID;
    int levelLenth;
    int levelGemCount;
    string levelName;

    public Slider gameProgress;
    bool secondChance;
    Vector3 camPos;

    public static UIHandler Instance;

    void Awake() 
    {
        if (!Instance) 
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    public void StartGame()
    {
        camPos = camera.transform.position;
        ResetActivateCompletedLevel();        
        StartCoroutine(ActivateUI());
    }
    IEnumerator ActivateUI() 
    {
        yield return new WaitForSeconds(5f);
        DissableScreens();
        if (GameManager.Instance.isFirstTime)
        {
            InfoScreen.SetActive(true);
            GameManager.Instance.isFirstTime = false;
        }
        else
        {
            StartScreen.SetActive(true);
        }
        
    }
    public void PlayGame() 
    {
        DissableScreens();
        StartScreen.SetActive(true);
        //GameManager.Instance.canPlay = true;
        // GamePlayScreen.SetActive(true);
    }
    public void GameOverUI() 
    {
        DissableScreens();
        GameOverScreen.SetActive(true);
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
    void DissableScreens() 
    {
        SplashScreen.SetActive(false);
        InfoScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        GamePlayScreen.SetActive(false);
        StartScreen.SetActive(false);
        GameWinScreen.SetActive(false);
        DemoCompleteScreen.SetActive(false);
    }
    public void GameWin() 
    {
        DissableScreens();
        GameWinScreen.transform.GetChild(1).GetComponent<Text>().text = levelName;
        GameWinScreen.SetActive(true);
        PlayCamAnimation();
        GameManager.Instance.playerGemCount += levelGemCount;
    }
    public void Nothanks() 
    {
        DissableScreens();
        ActivateStartScreen();
        Confetti.SetActive(false);
        camAnim.SetBool("CamAnim", false);
        GameManager.Instance.LoadLevel();
    }
    void PlayCamAnimation()
    {
        camAnim.SetBool("CamAnim",true);
        Confetti.SetActive(true);
    }
    void ActivateStartScreen() 
    {
        StartScreen.SetActive(true);
        gemCountText.text = ""+GameManager.Instance.playerGemCount;
    }
    public void SetGameInfo(LevelData currentLevelData) 
    {
        levelID = 0;
        levelLenth = 0;
        levelGemCount = 0;
        levelName ="";

        levelID = currentLevelData.levelID;
        levelLenth= currentLevelData.levelLenth;
        levelGemCount= currentLevelData.gemCount;
        levelName = currentLevelData.levelName;

        gameProgress.minValue = 0;
        gameProgress.maxValue = levelLenth;
        levelNameText.text = "";
        levelNameText.text = levelName;

    }
    public void DemoComplete()
    {
        DissableScreens();
        DemoCompleteScreen.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameStatus == GameManager.GameStatus.PLAYING) 
        {
            DissableScreens();
            GamePlayScreen.SetActive(true);
            gameProgress.value =PlayerMovements.Instance.player.transform.position.z;
            //print("gameProgress.value" + gameProgress.value);
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
