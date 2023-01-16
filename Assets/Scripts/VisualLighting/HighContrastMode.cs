/// used for implementing the high contrast mode for visual considerations 
/// however this is still in development -- needs to be worked on for future development
/// this currently does not work

using UnityEngine;

public class HighContrastMode : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Color originalColor;

 

    public void EnableHighContrastMode()
    {
        originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.black;
    }

    public void DisableHighContrastMode()
    {
        spriteRenderer.color = originalColor;
    }
}