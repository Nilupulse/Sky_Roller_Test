using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public bool moveRight;
    public bool moveLeft;
    public float sensitivity = 1.5f;
    static bool movement = true;

    bool isDraging;
    Vector2 startTouch;
    Vector2 swipeDelta;
    // Start is called before the first frame update
    void Start()
    {
        
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
        moveRight=false;
        moveLeft = false;
        // Get the input vector from keyboard or analog stick
        var directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, 0);


        


        //if no keyboard input... use mobile controls
        if (directionVector == Vector3.zero)
        {


            //check for moved or stationary finger
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
            {

                //check for change in direction every frame
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                //if direction is greater than sensitivity (1.5), set the movement to right, also set mobileRight to true... this will allow movement with stationary finger
                if (touchDeltaPosition.x > sensitivity)
                {

                    //allows for movement after finger stops moving
                    moveRight = true;
                }

                //else check to see if direction of finger movement is less than -sensitivity (-1.5) if so set direction to left and mobileRight to false
                else if (touchDeltaPosition.x < -sensitivity)
                {

                    // set mobileRight to false allowing later testing
                    moveLeft = true;
                }

                //if touch direction is 0 (Finger NOT moving)
                else if (touchDeltaPosition.x == 0)
                {

                    //check to see if last direction was right,... if so move right while finger direction is 0
                    if (moveRight)
                    {
                        //moveRight = true;
                    }
                    //if last direction was left move left as above
                    else
                    {

                    }
                }
            }

        }
        else 
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDraging = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDraging = false;
                Reset();
            }
            swipeDelta = Vector2.zero;
            if (isDraging)
            {
                if (Input.GetMouseButton(0))
                {
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
                }
            }
            if (swipeDelta.magnitude > 100)
            {
                float x = swipeDelta.x;
                float y = swipeDelta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                    {
                        moveLeft = true;
                    }
                    else
                    {
                        moveRight = true;
                    }
                }
                Reset();
            }
        }



        ////code for other part of game... allows me to disable movement quickly
        //if (!movement)
        //{
        //    directionVector = Vector3.zero;

        //}



        //if (directionVector != Vector3.zero)
        //{
        //    // Get the length of the directon vector and then normalize it
        //    // Dividing by the length is cheaper than normalizing when we already have the length anyway
        //    var directionLength = directionVector.magnitude;
        //    directionVector = directionVector / directionLength;

        //    // Make sure the length is no bigger than 1
        //    directionLength = Mathf.Min(1, directionLength);

        //    // Make the input vector more sensitive towards the extremes and less sensitive in the middle
        //    // This makes it easier to control slow speeds when using analog sticks
        //    directionLength = directionLength * directionLength;

        //    // Multiply the normalized direction vector by the modified length
        //    directionVector = directionVector * directionLength;
        //}
    }
}
