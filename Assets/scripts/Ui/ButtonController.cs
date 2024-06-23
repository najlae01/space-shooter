using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public GameObject confirmationScreen; // Reference to the confirmation screen UI object

    private bool controlModeConfirmed = false; // Flag to track if control mode is confirmed

    public void LoadLevelMenu(string levelName)
    {
        FadeCanvas.fader.FaderloadString(levelName);
    }

    public void LoadLevelString(string levelName)
    {
        Debug.Log("LoadLevelString called with level: " + levelName);
        LevelManager.levelManager.ControlModePanel();
        StartCoroutine(LoadLevelWithConfirmation(levelName));
        //FadeCanvas.fader.FaderloadString(levelName);
    }

    public void LoadLevelInt(int levelIndex)
    {
        Debug.Log("LoadLevelInt called with level index: " + levelIndex);
        LevelManager.levelManager.ControlModePanel();
        StartCoroutine(LoadLevelWithConfirmation(levelIndex));
        //FadeCanvas.fader.FaderloadInt(levelIndex);
    }

    public void Restartlevel()
    {
        LevelManager.levelManager.ControlModePanel();
        StartCoroutine(RestartLevelWithConfirmation());
    }

    public void ActivateControlMode()
    {
        LevelManager.levelManager.autoControlEnabled = true;
        controlModeConfirmed = true;
    }

    public void DontActivateControlMode()
    {
        LevelManager.levelManager.autoControlEnabled = false;
        controlModeConfirmed = true;
    }

    private IEnumerator LoadLevelWithConfirmation(string levelName)
    {
        confirmationScreen.SetActive(true); // Show the confirmation screen

        // Wait until control mode is confirmed
        while (!controlModeConfirmed)
        {
            yield return null;
        }

        confirmationScreen.SetActive(false); // Hide the confirmation screen

        if (controlModeConfirmed)
        {
            FadeCanvas.fader.FaderloadString(levelName); // Transition to the next level
        }
    }

    private IEnumerator LoadLevelWithConfirmation(int levelIndex)
    {
        confirmationScreen.SetActive(true); // Show the confirmation screen

        // Wait until control mode is confirmed
        while (!controlModeConfirmed)
        {
            yield return null;
        }

        confirmationScreen.SetActive(false); // Hide the confirmation screen

        if (controlModeConfirmed)
        {
            FadeCanvas.fader.FaderloadInt(levelIndex); // Transition to the next level
        }
    }

    private IEnumerator RestartLevelWithConfirmation()
    {
        confirmationScreen.SetActive(true); // Show the confirmation screen

        // Wait until control mode is confirmed
        while (!controlModeConfirmed)
        {
            yield return null;
        }

        confirmationScreen.SetActive(false); // Hide the confirmation screen

        if (controlModeConfirmed)
        {
            FadeCanvas.fader.FaderloadInt(SceneManager.GetActiveScene().buildIndex); // Restart the level
        }
    }
}
