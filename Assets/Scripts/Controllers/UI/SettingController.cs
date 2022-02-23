using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingController : MonoBehaviour
{
    public Button musicBtn;
    public Button vibrationBtn;

    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    public Sprite vibrationOnSprite;
    public Sprite vibrationOffSprite;

    private void Awake()
    {
        PlayerPrefs.GetInt(GameConstants.music, 1);
        PlayerPrefs.GetInt(GameConstants.vibration, 1);
    }
    private void OnEnable()
    {
       
        SetSettingBtnStates();
    }
    private void SetSettingBtnStates()
    {
        if (PlayerPrefs.GetInt(GameConstants.music, 1) == 1)
        {
            musicBtn.image.sprite = musicOnSprite;
        }
        else
        {
            musicBtn.image.sprite = musicOffSprite;
        }
        if (PlayerPrefs.GetInt(GameConstants.vibration, 1) == 1)
        {
            vibrationBtn.image.sprite = vibrationOnSprite;
        }
        else
        {
            vibrationBtn.image.sprite = vibrationOffSprite;
        }
        Events.DoFireOnChangeMusicState();
    }
    public void OnChangeMusic()
    {
        if (PlayerPrefs.GetInt(GameConstants.music, 1) == 1)
        {
            PlayerPrefs.SetInt(GameConstants.music,0);
        }
        else
        {
            PlayerPrefs.SetInt(GameConstants.music, 1);
        }
        SetSettingBtnStates();
        Events.DoFireOnPlayClickBtn();
    }
    public void OnChangeVibration()
    {
        if (PlayerPrefs.GetInt(GameConstants.vibration, 1) == 1)
        {
            PlayerPrefs.SetInt(GameConstants.vibration, 0);
        }
        else
        {
            PlayerPrefs.SetInt(GameConstants.vibration, 1);
        }
        SetSettingBtnStates();
        Events.DoFireOnPlayClickBtn();
    }

}
