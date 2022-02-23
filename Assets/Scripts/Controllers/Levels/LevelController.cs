using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class LevelController : MonoBehaviour
{
    public LevelHandler[] levels;
    public int levelIndex = 0;
    public static int currentLevelIndex = 0;
    public static int numberOfLevels = 0;
    public LevelUIController levelUIController;
    private void Awake()
    {
        numberOfLevels = levels.Length;
        currentLevelIndex = 0;
        levelIndex = PlayerPrefs.GetInt(GameConstants.levelCompleted,0);
    }

    private void OnEnable()
    {
        Events.OnSetLevel += Events_OnSetLevel;
        Events.OnSkipLevel += Events_OnSkipLevel;
    }

    private void Events_OnSkipLevel()
    {
        CompletedLevelFromAd();
    }

    private void Events_OnSetLevel()
    {
        SetLevel();
    }

    private void SetLevel()
    {
        try
        {
            for (int i = 0; i < levels.Length; i++)
            {
                levels[i].gameObject.SetActive(false);
                levels[i].SeThisLevelToDefaultValues();
            }
            levels[currentLevelIndex].gameObject.SetActive(true);
        }
        catch (System.Exception ex)
        {
            Debug.Log(" " + ex.Message + " " + currentLevelIndex);
        }
    }
    public void SetLevel(int index)
    {
        try { 
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].gameObject.SetActive(false);
            levels[i].SeThisLevelToDefaultValues();
        }
        levels[index].gameObject.SetActive(true);
        }
        catch(System.Exception ex)
        {
            Debug.Log(" "+ex.Message);
        }
    }
    public void LevelCompleted()
    {
        int count = 0;
      
        for (int j = 0; j < levels[currentLevelIndex].cars.Length; j++)
            {
                if (levels[currentLevelIndex].cars[j].isEscaped)
                    {
                        count++;
                    }
            }
 
        if(count == levels[currentLevelIndex].cars.Length)
        {
            // level Up
            Events.DoFireOnShowScreen(Screens.levelCompleteScreens);
            if(currentLevelIndex == PlayerPrefs.GetInt(GameConstants.levelCompleted))
                UpgradeLevelIndex();
            AdsManager.Instance.ShowInterstitial();
        }
        
    }
    public void UpgradeLevelIndex()
    {
        levelIndex ++;
        if (levelIndex >= levels.Length)
        {
            levelIndex = 0;
            PlayerPrefs.SetInt(GameConstants.levelCompleted, levelIndex);
            RestartLevel();
        }
        PlayerPrefs.SetInt(GameConstants.levelCompleted, levelIndex);
        levelUIController.UnlockSprites();

    }
    private void CompletedLevelFromAd()
    {
        Events.DoFireOnShowScreen(Screens.levelCompleteScreens);
        UpgradeLevelIndex();
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(GameConstants.levelName);
    }
    private void OnDisbale()
    {
        Events.OnSetLevel -= Events_OnSetLevel;
        Events.OnSkipLevel -= Events_OnSkipLevel;
    }
}
