/// used for implementing the high contrast mode for visual considerations 
/// however this is still in development -- needs to be worked on for future development
/// this currently does not work

using UnityEngine;
using UnityEngine.UI;

public class HighContrastToggle : MonoBehaviour
{
    public HighContrastMode highContrast;
    public Toggle toggle;
    private bool highContrastMode;

    private void Start()
    {
        toggle.onValueChanged.AddListener(ToggleHighContrastMode);
    }

    private void ToggleHighContrastMode(bool value)
    {
        highContrastMode = value;
        if (highContrastMode)
        {
            highContrast.EnableHighContrastMode();
        }
        else
        {
            highContrast.DisableHighContrastMode();
        }
    }
}