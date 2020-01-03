using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleHandler : MonoBehaviour
{
    public bool canRotate;
    public bool canMove;

    void Update() 
    {
        if (canRotate) // can create rotateble obsticals
        {
            transform.Rotate(new Vector3(0,1,0),180*Time.deltaTime);
        }
    }
    void OnCollisionEnter(Collision collision)
    {      
        if (collision.gameObject.tag== "Player") 
        {
            GameManager.Instance.canPlay = false;
            GameManager.Instance.gameStatus = GameManager.GameStatus.HIT;
            GameManager.Instance.SetHitObject(this.gameObject.transform.parent.gameObject);
            UIHandler.Instance.GameOverUI();
        }
    }
}
