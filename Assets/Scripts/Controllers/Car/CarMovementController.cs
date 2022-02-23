using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Movement
{
    forward,
    backward
}
public enum VehicleFacing
{
    Left,
    Right,
    Upward,
    Downward
}
public class CarMovementController : MonoBehaviour
{
    Rigidbody rigid;
    public float velocity = 30f;
    private bool start = false;
    public bool itsMe;
    public bool isOnRoad;

    public Movement currentMovement;
    public VehicleFacing vehicleStandingPose;
    public AudioHandler audioHandler;

    public bool isEscaped;
    Quaternion initialRotation;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        audioHandler = GetComponent<AudioHandler>();
        initialRotation = transform.localRotation;
        SetVehicleFacing();
    }

    private void LateUpdate()
    {
        SetVelocity();
    }
    private void SetVelocity()
    {
        if (start)
        {
            rigid.isKinematic = false;
            if (currentMovement == Movement.forward)
                rigid.velocity = transform.forward * velocity;
            else if (currentMovement == Movement.backward)
            { 
              
                rigid.velocity = transform.forward * -1 * velocity;
            }

        }
        else
        {
            rigid.isKinematic = true;
            rigid.velocity = transform.forward * -1 * 0;
        }
    }
    public void StartCar(bool isStart)
    {
        start = isStart;
        if(start)
        {
            Events.DoFireOnFingerSwipe();
            audioHandler.PlayEngineRunningSound();
        }
        else
        {
            audioHandler.StopEngineRunningSound();
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals(GameConstants.hurdleTag) || collision.gameObject.tag.Equals(GameConstants.vehicleTag))
        {
            audioHandler.PlayHittingSound();
            bool tempStart = start;
            if(!isOnRoad)
            StartCar(false);
            if (tempStart && !isOnRoad)
                GetJerk();
        }
       
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(GameConstants.roadcenterTag))
        {
            transform.forward = other.gameObject.transform.forward;
            isOnRoad = true;
            currentMovement = Movement.forward;
            gameObject.layer = 9;
        }
    }
    public void GetJerk()
    {
        if (vehicleStandingPose == VehicleFacing.Right)
        {
            if(currentMovement == Movement.forward)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);
            else if (currentMovement == Movement.backward)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
            }
        }
        else if (vehicleStandingPose == VehicleFacing.Left)
        {
            if (currentMovement == Movement.forward)
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
            else if (currentMovement == Movement.backward)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);
            }
        }
        else if (vehicleStandingPose == VehicleFacing.Upward)
        {
            if (currentMovement == Movement.forward)
                transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
            else if (currentMovement == Movement.backward)
            {
                transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
            }
        }
        else if (vehicleStandingPose == VehicleFacing.Downward)
        {
            if (currentMovement == Movement.forward)
                transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
            else if (currentMovement == Movement.backward)
            {
                transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
            }
        }
        if(PlayerPrefs.GetInt(GameConstants.vibration) == 1)
            Handheld.Vibrate();
    }
    void SetVehicleFacing()
    {
       
        switch(transform.localEulerAngles.y)
        {
            case 0:
                vehicleStandingPose = VehicleFacing.Right;
                break;
            case 180:
                vehicleStandingPose = VehicleFacing.Left;
                break;
            case 90:
                vehicleStandingPose = VehicleFacing.Downward;
                break;
            case 270:
                vehicleStandingPose = VehicleFacing.Upward;
                break;

        }
    }

}
