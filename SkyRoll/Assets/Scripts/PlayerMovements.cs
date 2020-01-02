using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovements : MonoBehaviour
{
    public GameObject playerLeft;
    public GameObject playerRight;
    public GameObject player;
    public float playerSpeed;
    float playerCurrentSpeed;
    public float playerMoveSpeed;
    bool boostMode;

    Vector3 playerPos;
    Vector3 playerLeftPos;
    Vector3 playerRightPos;

    public static PlayerMovements Instance;

    public Text Test;
    void Awake() 
    {
        if (!Instance) 
        {
            Instance = this;
        }
    }
    void Start() 
    {
        boostMode = false;
        playerCurrentSpeed = playerSpeed;
        playerPos = player.transform.position;
        playerLeftPos=playerLeft.transform.position;
        playerRightPos= playerRight.transform.position;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.canPlay)
        {
            player.transform.Translate(0, 0, playerSpeed * Time.deltaTime);
        }

        //if (Input.touchCount>0) 
        //{
        //    Touch touch = Input.GetTouch(0);
        //    Test.text = ""+touch.deltaPosition;
        //}



        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (GameManager.Instance.gameStatus == GameManager.GameStatus.STANDBY || GameManager.Instance.gameStatus == GameManager.GameStatus.PLAYING)
            {
               
                GameManager.Instance.canPlay = true;
                GameManager.Instance.gameStatus = GameManager.GameStatus.PLAYING;

                if (playerLeft.transform.position.x < -.3f)
                {
                    playerLeft.transform.Translate(playerMoveSpeed * Time.deltaTime, 0, 0);
                    playerRight.transform.Translate(-playerMoveSpeed * Time.deltaTime, 0, 0);
                }
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (GameManager.Instance.gameStatus == GameManager.GameStatus.STANDBY || GameManager.Instance.gameStatus == GameManager.GameStatus.PLAYING)
            {
                GameManager.Instance.canPlay = true;
                GameManager.Instance.gameStatus = GameManager.GameStatus.PLAYING;

                if (playerLeft.transform.position.x > -1f)
                {
                    playerLeft.transform.Translate(-playerMoveSpeed * Time.deltaTime, 0, 0);
                    playerRight.transform.Translate(playerMoveSpeed * Time.deltaTime, 0, 0);
                }
            }
        }
    }
    
    public void BoostSpeed() 
    {
        boostMode = true;
        
        playerSpeed = 8f;
        StartCoroutine(EndBoost());
    }
    IEnumerator EndBoost() 
    {
        yield return new WaitForSeconds(GameManager.Instance.boostTime);
        boostMode = false;
        playerSpeed = playerCurrentSpeed;

    }
    public void ResetPlayer() 
    {
        player.transform.position= playerPos;
        playerLeft.transform.position= playerLeftPos;
        playerRight.transform.position= playerRightPos;
        playerSpeed = playerCurrentSpeed;
    }
    public void SlowDownPlayer() 
    {
        
        if (playerSpeed > 0)
        {
            if (boostMode)
            {
                playerSpeed = playerCurrentSpeed;
            }
            playerSpeed -= .3f;
            Invoke("SlowDownPlayer", .1f);
        }
        else 
        {
            playerSpeed = 0;
            GameManager.Instance.canPlay = false;            
        }
        
    }
}
