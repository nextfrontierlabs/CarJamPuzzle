using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{ 
        public CarMovementController[] cars;
        public DefaultValueController[] defaultValuesOFCars;

    private void Awake()
    {
        SetLevelCars(transform.GetComponentsInChildren<CarMovementController>());
        SetDefaultValuesOFCars(transform.GetComponentsInChildren<DefaultValueController>());
    }
    private void SetLevelCars(CarMovementController[] carsArg)
        {
            cars = carsArg;
        }
    private void SetDefaultValuesOFCars(DefaultValueController[] carsArg)
    {
        defaultValuesOFCars = carsArg;
    }
    public  void SeThisLevelToDefaultValues()
    {
        foreach (var item in defaultValuesOFCars)
        {
            item.GetDefaultValueOfComponentsFromHashtable();
        }
    }

}
