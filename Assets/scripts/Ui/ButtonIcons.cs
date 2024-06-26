using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonIcons : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Sprite unlockedIcon;
    [SerializeField] private Sprite lockedIcon;
    [SerializeField] private int firstLevelBuildIndex;

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt(EndGameManager.endManager.lvlUnlock, firstLevelBuildIndex);
        for(int i = 0; i < levelButtons.Length; i++)
        {
            if(i + firstLevelBuildIndex <= unlockedLevel)
            {
                levelButtons[i].interactable = true;
                levelButtons[i].image.sprite = unlockedIcon;
                TextMeshProUGUI textButton = levelButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                textButton.text = (i + 1).ToString();
                textButton.enabled = true;
            }
            else
            {
                levelButtons[i].interactable = false;
                levelButtons[i].image.sprite = lockedIcon;
                levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(" ");
            }
        }
    }
}
