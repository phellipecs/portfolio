using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouse : MonoBehaviour {

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private void OnMouseOver()
    {
        Cursor.SetCursor(null, hotSpot, cursorMode);
    }
}
