using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DefaultValues
{
    CarMovement,
    Position,
    Rotation
}
public class DefaultValueController : MonoBehaviour
{
    [SerializeField]
    CarMovementController thisCarMovement;
    public Hashtable defaultValues;
    private void Awake()
    {
        defaultValues = new Hashtable();
        thisCarMovement = gameObject.GetComponent<CarMovementController>();
        SetDefaultValueOfComponentsIntoHashtable();
    }
    public void SetDefaultValueOfComponentsIntoHashtable()
    {
        defaultValues.Add(DefaultValues.CarMovement, thisCarMovement);
        defaultValues.Add(DefaultValues.Position, transform.localPosition);
        defaultValues.Add(DefaultValues.Rotation, transform.localRotation);
    }
    public void GetDefaultValueOfComponentsFromHashtable()
    {
            GetCarMovmentFromHashTable();
            GetCarRotationFromHashTable();
            GetCarPositionFromHashTable();

    }
    private void GetCarMovmentFromHashTable()
    {
        try { 
             thisCarMovement.StartCar(false);
             thisCarMovement.isEscaped = false;
             thisCarMovement.isOnRoad = false;
             thisCarMovement.itsMe = false;
             gameObject.layer = 8;
        }
        catch(System.Exception ex)
        {
            Debug.Log(ex.Message);

        }
    }
    private void GetCarRotationFromHashTable()
    {

        transform.localRotation = (Quaternion)defaultValues[DefaultValues.Rotation];
    }
    private void GetCarPositionFromHashTable()
    {

        transform.localPosition = (Vector3)defaultValues[DefaultValues.Position];
    }

}
