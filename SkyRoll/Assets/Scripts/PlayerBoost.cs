using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoost : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        print("Boostssss");
        if (other.gameObject.tag == "Player")
        {
            print("Boost");
            PlayerMovements.Instance.BoostSpeed();
        }
    }
}
