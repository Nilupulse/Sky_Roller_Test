using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerHatCollect : MonoBehaviour
{
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("Hat power uo collected");
            GameManager.Instance.StartCoroutine("HatPowerUp");
            Destroy(this.gameObject);
        }
    }
}
