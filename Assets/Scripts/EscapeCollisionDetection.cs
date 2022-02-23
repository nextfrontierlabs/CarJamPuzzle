using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeCollisionDetection : MonoBehaviour
{
    public LevelController levelController;
    public AudioSource[] carAudioSource;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == GameConstants.vehicleTag)
        {
            collision.gameObject.GetComponent<CarMovementController>().isEscaped = true;
            if (PlayerPrefs.GetInt(GameConstants.vibration) == 1)
                Handheld.Vibrate();
            levelController.LevelCompleted();
          
        }
    }
  

}
