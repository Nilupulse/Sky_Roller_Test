using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
    bool swipeLeft;
    bool swipeRight;
    bool tap;
    bool isDraging;
    Vector2 startTouch;
    Vector2 swipeDelta;
    public Text Test;



    public Vector2 SwipeDelta
    {
        get 
        {
            return swipeDelta;
        }
    }
    public bool SwipeLeft
    {
        get
        {
            return swipeLeft;
        }
    }
    public bool SwipeRight
    {
        get
        {
            return swipeRight;
        }
    }
    public Vector2 StartTouch
    {
        get
        {
            return startTouch;
        }
    }
    void Reset() 
    {
        startTouch = Vector2.zero;
        swipeDelta = Vector2.zero;
        //isDraging = false;
    }

    // Update is called once per frame
    void Update()
    {
        swipeLeft = false;
        swipeRight = false;
        tap = false;

        #region Standalone Input
        if (Input.GetMouseButtonDown(0))
        {
            isDraging = true;
            tap = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }
        #endregion

        //#region Mobile Input
        //if (Input.touches.Length > 0)
        //{
        //    if (Input.touches[0].phase == TouchPhase.Began)
        //    {
        //        isDraging = true;
        //        tap = true;
        //        startTouch = Input.touches[0].position;
        //    }
        //    else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) 
        //    {
        //        isDraging = false;
        //        Reset();
        //    }
        //}
        //#endregion

        swipeDelta = Vector2.zero;
        if (isDraging) 
        {
            //if (Input.touches.Length > 0)
            //{
            //    swipeDelta = Input.touches[0].position - startTouch;
            //}
            if (Input.GetMouseButton(0)) 
            {
                swipeDelta =(Vector2) Input.mousePosition- startTouch;
            }
        }

        if (swipeDelta.magnitude>100) 
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            Test.text += "X " + swipeDelta.x;

            if (Mathf.Abs(x)>Mathf.Abs(y)) 
            {
                if (x < 0)
                {
                    swipeLeft = true;
                }
                else 
                {
                    swipeRight = true;
                }
            }
            Reset();
        }

       
    }
}
