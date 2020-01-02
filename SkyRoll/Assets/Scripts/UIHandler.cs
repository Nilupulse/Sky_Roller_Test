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
    public GameObject Confetti;
    public Image[] completedLevels = new Image[4];
    public Text playerScoreText;
    public Animator camAnim;
    public GameObject camera;
    public Slider gameProgress;
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
        SetGameProgress();
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
        GameManager.Instance.ReviveGame();
        DissableScreens();
        StartScreen.SetActive(true);
    }
    public void GameOver()
    {
        GameManager.Instance.gameStatus = GameManager.GameStatus.GAMEOVER;
        DissableScreens();
        StartScreen.SetActive(true);
        GameManager.Instance.LoadLevel();
    }
    public void ActivateCompletedLevel(int levelID) 
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
    }
    public void GameWin(string levelName) 
    {
        DissableScreens();
        GameWinScreen.transform.GetChild(1).GetComponent<Text>().text = levelName;
        GameWinScreen.SetActive(true);
        PlayCamAnimation();
    }
    public void Nothanks() 
    {
        DissableScreens();
        StartScreen.SetActive(true);
        Confetti.SetActive(false);
        camAnim.SetBool("CamAnim", false);
        GameManager.Instance.LoadLevel();
    }
    void PlayCamAnimation()
    {
        camAnim.SetBool("CamAnim",true);
        Confetti.SetActive(true);
    }
    public void SetGameProgress() 
    {
        gameProgress.minValue = 0;
        gameProgress.maxValue = 49;

    }
    // Update is called once per frame
    void Update()
    {
        playerScoreText.text = ""+GameManager.Instance.playerScore;
        if (GameManager.Instance.gameStatus == GameManager.GameStatus.PLAYING) 
        {
            DissableScreens();
            GamePlayScreen.SetActive(true);
            print("X" + PlayerMovements.Instance.player.transform.position.z);
            gameProgress.value =PlayerMovements.Instance.player.transform.position.z;
            print("gameProgress.value" + gameProgress.value);
        }
    }
}
