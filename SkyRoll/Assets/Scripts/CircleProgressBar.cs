using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleProgressBar : MonoBehaviour
{
    public Image progressBar;
    public float chanceTime;
    public GameObject btnNoThanks;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        progressBar.fillAmount += 1f / 10f * Time.deltaTime;
        if (progressBar.fillAmount==1)
        {
            btnNoThanks.SetActive(true);
        }
    }
}
