using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{

    public GameObject[] fingerCanvas;

    private int fingerCounter;

    private void OnEnable()
    {
        Events.OnFingerSwipe += Events_OnFingerSwipe;
        fingerCanvas[0].SetActive(true);
        fingerCanvas[1].SetActive(false);
    }

    private void Events_OnFingerSwipe()
    {
        fingerCounter++;

        switch(fingerCounter)
        {
            case 1:
                fingerCanvas[0].SetActive(false);
                fingerCanvas[1].SetActive(true);
                break;
            case 2:
                fingerCanvas[0].SetActive(false);
                fingerCanvas[1].SetActive(false);
                break;
        }
    }

    private void OnDisable()
    {
        Events.OnFingerSwipe -= Events_OnFingerSwipe;
    }
}
