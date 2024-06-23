using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;

    private AvoidEnnemiesAgent agent;
    private PlayerControls playerControls;

    public bool autoControlEnabled = false;

    private PanelController panelController;

    private void Awake()
    {
        if (levelManager == null)
        {
            levelManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Find and assign the AvoidEnnemiesAgent component
        agent = FindFirstObjectByType<AvoidEnnemiesAgent>();

        // Find and assign the PlayerControls component
        playerControls = FindFirstObjectByType<PlayerControls>();
    }

    public void RegisterPanelController(PanelController panelC)
    {
        panelController = panelC;
    }

    public void ToggleControlMode()
    {
        autoControlEnabled = !autoControlEnabled;

        if (autoControlEnabled)
        {
            // Enable auto control mode
            if (agent != null)
                agent.enabled = true;

            if (playerControls != null)
                playerControls.enabled = false;
        }
        else
        {
            // Enable manual control mode
            if (agent != null)
                agent.enabled = false;

            if (playerControls != null)
                playerControls.enabled = true;
        }
    }

    public void ControlModePanel()
    {
        panelController.ActivateControlModeScreen();
    }

}
