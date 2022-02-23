using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    public List<Button> buttons;
    public List<Text> texts;
    public Sprite unlock;
    public Sprite locked;

    public void FindChildren()
    {
        buttons = GetComponentsInChildren<Button>().ToList();
        texts = GetComponentsInChildren<Text>().ToList();
    }
    public void AddOnClickListener(int index)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int num = index + i;
            buttons[i].onClick.AddListener(delegate { OnClickLevelButton(num); });
            texts[i].text = (num+1).ToString();
            SetButtonsSprite(num,i);
        }
    }

    private void SetButtonsSprite(int num,int i)
    {
        if (num <= PlayerPrefs.GetInt(GameConstants.levelCompleted))
        {
            buttons[i].image.sprite = unlock;
            buttons[i].interactable = true;
        }
        else
        {
            buttons[i].image.sprite = locked;
            buttons[i].interactable = false;
        }
    }
    public void SetButtonsSprite(int index)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int num = index + i;
            if (num <= PlayerPrefs.GetInt(GameConstants.levelCompleted))
            {
                buttons[i].image.sprite = unlock;
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].image.sprite = locked;
                buttons[i].interactable = false;
            }
        }
    }
    public void OnClickLevelButton(int index)
    {
        LevelController.currentLevelIndex = index;
        Events.DoFireOnShowScreen(Screens.none);
        Events.DoFireOnSetLevel();
        Events.DoFireOnPlayClickBtn();

    }
}
