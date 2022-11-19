using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOverlay : MonoBehaviour
{
    public Texture2D cursorPoint;
    public Texture2D cursorClick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnMouseEnter()
    {
        Cursor.SetCursor(cursorClick, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(cursorPoint, Vector2.zero, CursorMode.Auto);
    }
}
