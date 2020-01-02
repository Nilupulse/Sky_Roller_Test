using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    public List<GameObject> obsticalPass =new List<GameObject>();
  
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pass") 
        {
            print("AS");
            foreach (GameObject obstacle in obsticalPass) 
            {
                obstacle.SetActive(true);
            }            
            GameManager.Instance.playerScore++;
        }        
    }
}
