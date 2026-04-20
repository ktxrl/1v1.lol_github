using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player2hand : MonoBehaviour
{
    void Start()
    {
        //Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePos.x, mousePos.y);
    }
}
