using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MouseControl used to control the game with mouse
public class MouseControl : MonoBehaviour
{
    [SerializeField] private Texture2D crosshairTexture; // using Texture2D because SetCursor method requires it
    private Vector2 cursorHotspot = Vector2.zero; // hotspot is the point in the cursor that is the target point
    private CursorMode cursorMode = CursorMode.Auto; // set to auto so that the cursor will be displayed as the OS default cursor

    // Start is called before the first frame update
    private void Start()
    {
        SetCursor();
    }

    // method to set the cursor display to use the crosshair texture
    private void SetCursor()
    {
        if (crosshairTexture != null)
        {
            Cursor.SetCursor(crosshairTexture, cursorHotspot, cursorMode);
        }
    }
}
