using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class LevelScreenControls : MonoBehaviour
{
    [SerializeField] public Toggle switchControlToggle;
    public Text controlModeText;

    private LevelManager levelManager;

    private bool isTouchActive;
    private Vector3 touchStartPosition;

    private void Start()
    {
        levelManager = LevelManager.levelManager;

        switchControlToggle.onValueChanged.AddListener(OnToggleValueChanged);
        UpdateControlModeText();
    }

    private void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            Touch touch = Touch.activeTouches[0];
            Vector3 touchPos = touch.screenPosition;

            if (!isTouchActive && touch.phase == TouchPhase.Began && IsTouchOverToggle(touchPos))
            {
                isTouchActive = true;
                touchStartPosition = touchPos;
            }

            if (isTouchActive && touch.phase == TouchPhase.Moved)
            {
                Vector3 touchDelta = touchPos - touchStartPosition;
                if (touchDelta.magnitude >= 10f)
                {
                    isTouchActive = false;
                    ToggleControlMode();
                }
            }
        }
        else
        {
            isTouchActive = false;
        }
    }

    private void OnToggleValueChanged(bool value)
    {
        levelManager.autoControlEnabled = value;
        UpdateControlModeText();
    }

    private void ToggleControlMode()
    {
        levelManager.autoControlEnabled = !levelManager.autoControlEnabled;
        switchControlToggle.isOn = levelManager.autoControlEnabled;
        UpdateControlModeText();
    }

    private void UpdateControlModeText()
    {
        if (levelManager.autoControlEnabled)
        {
            controlModeText.text = "Auto Control";
        }
        else
        {
            controlModeText.text = "Manual Control";
        }
    }

    private bool IsTouchOverToggle(Vector3 touchPosition)
    {
        RectTransform toggleRect = switchControlToggle.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(toggleRect, touchPosition);
    }
}
