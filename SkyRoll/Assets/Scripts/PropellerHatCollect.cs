using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerHatCollect : MonoBehaviour
{
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            GameManager.Instance.StartCoroutine("HatPowerUp");
        }
    }
}
