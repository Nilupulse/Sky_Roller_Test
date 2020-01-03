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
            foreach (GameObject obstacle in obsticalPass) 
            {
                obstacle.SetActive(true);
            }
            SoundManager.Instance.ItemCollected();//play item collected sound
        }        
    }
}
