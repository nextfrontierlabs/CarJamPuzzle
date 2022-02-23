using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Screens
{
    mainScreen,
    levelScreen,
    levelCompleteScreens,
    levelPauseScreens,
    settingScreen,
    exitScreen,
    none,

}
public class UIController : MonoBehaviour
{
    public static Screens currentScreen = Screens.mainScreen;
    private static bool isSettingFromMainScreen;
    public GameObject[] screens;
    private void OnEnable()
    {
        Events.OnShowScreen += Events_OnShowScreen;
    }

    private void Events_OnShowScreen(Screens obj)
    {
        currentScreen = obj;
        ShowScreen((int)obj);
    }

    public void ShowScreen(int index)
    {
        for (int i = 0; i < screens.Length; i++)
        {
            screens[i].SetActive(false);
        }
   
        screens[index].SetActive(true);
       
    }

    public void PlayNextLevel()
    {
        LevelController.currentLevelIndex++;

        if (LevelController.currentLevelIndex >= 30)
        {
            LevelController.currentLevelIndex = 0;
        }
        Events.DoFireOnSetLevel();
        Events.DoFireOnShowScreen(Screens.none);
        Events.DoFireOnPlayClickBtn();
    }
    public void ResetGame()
    {
        PlayerPrefs.SetInt(GameConstants.levelCompleted, 0);
        RestartLevel();
        Events.DoFireOnPlayClickBtn();
    }
    public void PlayGame()
    {
        Events.DoFireOnShowScreen(Screens.levelScreen);
        Events.DoFireOnPlayClickBtn();
    }
    public void RestartLevel()
    {
        LevelController.currentLevelIndex--;
        if (LevelController.currentLevelIndex < 0)
        {
            LevelController.currentLevelIndex = 0;
        }
        Events.DoFireOnSetLevel();
        Events.DoFireOnShowScreen(Screens.none);
        Events.DoFireOnPlayClickBtn();
    }
    public void RestartLevelFromPause()
    {
        Events.DoFireOnSetLevel();
        Events.DoFireOnShowScreen(Screens.none);
        Events.DoFireOnPlayClickBtn();
    }
    public void GoHomeScreen()
    {
        Events.DoFireOnShowScreen(Screens.mainScreen);
        Events.DoFireOnPlayClickBtn();
       
    }
    public void PauseScreen()
    {
        Events.DoFireOnShowScreen(Screens.levelPauseScreens);
        Events.DoFireOnPlayClickBtn();
        AdsManager.Instance.ShowInterstitial();
    }
    public void SettingScreen(bool isFromMainScreen)
    {
        isSettingFromMainScreen = isFromMainScreen;
        Events.DoFireOnShowScreen(Screens.settingScreen);
        Events.DoFireOnPlayClickBtn();
        AdsManager.Instance.ShowInterstitial_Static();

    }
    public void Resume()
    {

        Events.DoFireOnShowScreen(Screens.none);
        Events.DoFireOnPlayClickBtn();
    }
    public void CloseSettingPanel()
    {
        if (isSettingFromMainScreen)
            Events.DoFireOnShowScreen(Screens.mainScreen);
        else
        Events.DoFireOnShowScreen(Screens.none);
        Events.DoFireOnPlayClickBtn();
    }
    public void ShowQuit()
    {
        Events.DoFireOnShowScreen(Screens.exitScreen);
        Events.DoFireOnPlayClickBtn();
        AdsManager.Instance.ShowInterstitial_Static();
    }
    public void YesQuit()
    {
        Events.DoFireOnPlayClickBtn();
        Application.Quit();
    }
    public void No()
    {
        Events.DoFireOnShowScreen(Screens.mainScreen);
        Events.DoFireOnPlayClickBtn();
    }

    public void SkipLevelByAd()
    {
        AdsManager.Instance.ShowRewardedAd();
    }
    private void OnDisable()
    {
        Events.OnShowScreen -= Events_OnShowScreen;
    }
}
