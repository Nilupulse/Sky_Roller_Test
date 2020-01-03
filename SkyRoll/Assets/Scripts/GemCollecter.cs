using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollecter : MonoBehaviour
{
    // Start is called before the first frame update
    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Destroy(this.gameObject);
    //        print("Gem Found");
    //    }
    //}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Gem Found");
        }
    }
}
