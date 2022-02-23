using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIController : MonoBehaviour
{
    public Transform parentContent;
    public GameObject levelItemObject;

    private void Start()
    {
        GenerateLevelItemsInScrollView();
    }
    public void GenerateLevelItemsInScrollView()
    {
        int index = 0;
        int counter = LevelController.numberOfLevels / 3;
        for (int i = 0; i < 10; i++)
        {
            GameObject lvlItmObj = Instantiate(levelItemObject,parentContent) as GameObject;
            LevelItem item = lvlItmObj.GetComponent<LevelItem>();
            item.FindChildren();
            item.AddOnClickListener(index);
            index += 3;
        }
    }
    public void UnlockSprites()
    {
        int index = 0;
        LevelItem[] items = parentContent.GetComponentsInChildren<LevelItem>();
        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetButtonsSprite(index);
            index += 3;
        }
    }
}
