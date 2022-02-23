using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneHandler : MonoBehaviour
{
    public Slider loadingSlider;
    private AsyncOperation asyncOperation;
    private void LoadScene()
    {
        asyncOperation =  SceneManager.LoadSceneAsync(GameConstants.levelName);
    }
    private void Update()
    {
        if(asyncOperation!=null)
        {
            loadingSlider.value =  asyncOperation.progress;
        }
    }
}
