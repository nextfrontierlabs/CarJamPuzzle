using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public CarMovementController carMovementControl; 
    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;

    public bool detectSwipeAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Awake()
    {
        carMovementControl = GetComponent<CarMovementController>();
    }
    void Update()
    {
        if(UIController.currentScreen == Screens.none)
        TouchAndSetPosition();
    }
    private void TouchAndSetPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fingerUpPos = Input.mousePosition;
            fingerDownPos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            RaycastHit firstHit;

            if (Physics.Raycast(ray, out firstHit))
            {
                if (firstHit.collider.gameObject == this.gameObject)
                {
                    carMovementControl.itsMe = true;
                }
                else
                {
                    carMovementControl.itsMe = false;
                }

            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            if (carMovementControl.itsMe)
            {
                fingerDownPos = Input.mousePosition;
                DetectSwipe();
            }
        }
    }
    void DetectSwipe()
    {

        if (VerticalMoveValue() > SWIPE_THRESHOLD && VerticalMoveValue() > HorizontalMoveValue())
        {
            Debug.Log("Vertical Swipe Detected!");
            if (fingerDownPos.y - fingerUpPos.y > 0)
            {
                OnSwipeUp();
            }
            else if (fingerDownPos.y - fingerUpPos.y < 0)
            {
                OnSwipeDown();
            }
            fingerUpPos = fingerDownPos;

        }
        else if (HorizontalMoveValue() > SWIPE_THRESHOLD && HorizontalMoveValue() > VerticalMoveValue())
        {
            Debug.Log("Horizontal Swipe Detected!");
            if (fingerDownPos.x - fingerUpPos.x > 0)
            {
                OnSwipeRight();
            }
            else if (fingerDownPos.x - fingerUpPos.x < 0)
            {
                OnSwipeLeft();
            }
            fingerUpPos = fingerDownPos;

        }
        else
        {
            Debug.Log("No Swipe Detected!");
        }
    }

    float VerticalMoveValue()
    {
        return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
    }

    float HorizontalMoveValue()
    {
        return Mathf.Abs(fingerDownPos.x - fingerUpPos.x);
    }

    void OnSwipeUp()
    {
        Debug.Log("Do something when swiped up");
        carMovementControl.StartCar(true);
        SetCurrentMovement(false);
    }

    void OnSwipeDown()
    {
        Debug.Log("Do something when swiped down");

        carMovementControl.StartCar(true);
        SetCurrentMovement(true);
    }

    void OnSwipeLeft()
    {
        Debug.Log("Do something when swiped left");

        carMovementControl.StartCar(true);
        SetCurrentMovement(true);
    }

    void OnSwipeRight()
    {
        Debug.Log("Do something when swiped right");

        carMovementControl.StartCar(true);
        SetCurrentMovement(false);
    }
    private void SetCurrentMovement(bool inverted)
    {
        if (inverted)
        {
            if (carMovementControl.vehicleStandingPose == VehicleFacing.Upward || carMovementControl.vehicleStandingPose == VehicleFacing.Right)
                carMovementControl.currentMovement = Movement.backward;
            else
            {
                carMovementControl.currentMovement = Movement.forward;
            }
        }
        else
        {
            if (carMovementControl.vehicleStandingPose == VehicleFacing.Upward || carMovementControl.vehicleStandingPose == VehicleFacing.Right)
                carMovementControl.currentMovement = Movement.forward;
            else
            {
                carMovementControl.currentMovement = Movement.backward;
            }
        }
    }
}
