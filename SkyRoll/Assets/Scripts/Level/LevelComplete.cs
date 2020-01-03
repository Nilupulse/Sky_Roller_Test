using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {       
        if (other.gameObject.tag == "Pass")
        {           
            GameManager.Instance.LevelCompleted();
        }
    }
}
