using UnityEngine;

public class CoordinateManager
{
    public static Vector2 GetCursorPositionInWorldPoint()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return cursorPosition;
    }
}