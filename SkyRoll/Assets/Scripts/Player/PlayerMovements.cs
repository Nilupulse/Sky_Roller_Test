using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovements : MonoBehaviour
{
    //Singleton class
    public static PlayerMovements Instance;

    public GameObject playerLeft;
    public GameObject playerRight;
    public GameObject player;
    public GameObject playerLeftLeg;
    public GameObject playerRightLeg;
    public GameObject playerBody;
    public GameObject leftTail;
    public GameObject rightTail;
    public float playerSpeed;
    public float playerMoveSpeed;
    float playerCurrentSpeed;
    bool boostMode;
    bool powerUpMode;

    Vector3 playerPos;
    Vector3 playerLeftPos;
    Vector3 playerRightPos;

    public Swipe swipeClass;
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
        print("playerSpeed Start " + playerSpeed);
        DataHandler.Instance.SaveData("PlayerSpeed", playerCurrentSpeed);
        playerPos = player.transform.position;
        playerLeftPos = playerLeft.transform.position;
        playerRightPos = playerRight.transform.position;
    }

    void FixedUpdate()
    {
        //print("canPlay"+ GameManager.Instance.canPlay);
        if (GameManager.Instance.canPlay)//player movement
        {
            //print("A");
            player.transform.Translate(0, 0, playerSpeed * Time.deltaTime);
        }
        //user Input handlling
        if (swipeClass.moveLeft || Input.GetKey(KeyCode.LeftArrow))
        {
            if (GameManager.Instance.gameStatus == GameManager.GameStatus.STANDBY || GameManager.Instance.gameStatus == GameManager.GameStatus.PLAYING)
            {               
                GameManager.Instance.canPlay = true;
                GameManager.Instance.gameStatus = GameManager.GameStatus.PLAYING;
                print("playerLeftLeg.transform.rotation"+ playerLeftLeg.transform.rotation);
                print("playerRightLeg.transform.rotationn" + playerRightLeg.transform.rotation);
                if (playerLeft.transform.position.x < -.3f)
                {
                    playerLeft.transform.Translate(playerMoveSpeed * Time.deltaTime, 0, 0);
                    playerRight.transform.Translate(-playerMoveSpeed * Time.deltaTime, 0, 0);

                    playerLeftLeg.transform.Rotate(new Vector3(0, 0, 1), -150 * Time.deltaTime);
                    playerRightLeg.transform.Rotate(new Vector3(0, 0, 1), 150 * Time.deltaTime);

                    playerBody.transform.localPosition = new Vector3(0, 1f * Time.deltaTime, 0);
                }
            }
        }
        if (swipeClass.moveRight || Input.GetKey(KeyCode.RightArrow))
        {
            if (GameManager.Instance.gameStatus == GameManager.GameStatus.STANDBY || GameManager.Instance.gameStatus == GameManager.GameStatus.PLAYING)
            {
                GameManager.Instance.canPlay = true;
                GameManager.Instance.gameStatus = GameManager.GameStatus.PLAYING;
                //Down
                if (playerLeft.transform.position.x > -1f)
                {
                    playerLeft.transform.Translate(-playerMoveSpeed * Time.deltaTime, 0, 0);
                    playerRight.transform.Translate(playerMoveSpeed * Time.deltaTime, 0, 0);

                    playerLeftLeg.transform.Rotate(new Vector3(0, 0, 1), 150 * Time.deltaTime);
                    playerRightLeg.transform.Rotate(new Vector3(0, 0, 1), -150 * Time.deltaTime);

                    playerBody.transform.localPosition = new Vector3(0,-12f*Time.deltaTime,0);                    
                }

            }
        }
    }
    public void PropellerHat() //boost player
    {
        powerUpMode = true;
        leftTail.SetActive(false);
        rightTail.SetActive(false);
        player.transform.position = new Vector3(player.transform.position.x, .29f, player.transform.position.z);
    }
    public void BoostSpeed() //boost player
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
        playerSpeed = DataHandler.Instance.GetFloatData("PlayerSpeed");
        print("playerSpeed"+ DataHandler.Instance.GetFloatData("PlayerSpeed"));
    }
    public void SlowDownPlayer() 
    {
        if (powerUpMode)
        {
            player.transform.position = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        }
        if (playerSpeed > 0)
        {
            print("Breaking...");
            if (boostMode)
            {
                playerSpeed = playerCurrentSpeed;
            }
            playerSpeed -= .6f;
            Invoke("SlowDownPlayer", .1f);
        }
        else 
        {
            print("Stoped...");
            playerSpeed = 0;
            powerUpMode = false;
            GameManager.Instance.canPlay = false;
            UIHandler.Instance.GameWin();
        }
        
    }
}
