using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject activateControlModeScreen;
    [SerializeField] private CanvasGroup canvasGroup;
    
    void Start()
    {
        EndGameManager.endManager.RegisterPanelController(this);    
        LevelManager.levelManager.RegisterPanelController(this);
    }

    public void ActivateWin()
    {
        canvasGroup.alpha = 1.0f;
        winScreen.SetActive(true);
    }

    public void ActivateLose()
    {
        canvasGroup.alpha = 1.0f;
        loseScreen.SetActive(true);
    }

    public void ActivateControlModeScreen()
    {
        canvasGroup.alpha = 1.0f;
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
        activateControlModeScreen.SetActive(true);
    }

    void Update()
    {
        
    }
}
