using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public Texture2D cursorTexture; // Textura do cursor que você deseja usar
    public Vector2 hotspot = Vector2.zero; // Ponto quente do cursor (ponto de clique)

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
