using UnityEngine;
using System.Collections;

public class mouseUI : MonoBehaviour {
    [SerializeField] Texture2D cursorTexture;
    [SerializeField][HideInInspector] CursorMode cursorMode = CursorMode.Auto;
    [SerializeField][HideInInspector] Vector2 hotSpot = Vector2.zero;
    void Start() {
        //Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

}
