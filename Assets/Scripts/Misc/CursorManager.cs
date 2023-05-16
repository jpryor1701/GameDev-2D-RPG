using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();    
    }

    void Start()
    {
        Cursor.visible = false; // disable OS cursor

        if (Application.isPlaying)
        {
            Cursor.lockState = CursorLockMode.None;            
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // prevents cursor from leaving the window    
        }
    }

    void Update()
    {
        Vector2 cursorPos = Input.mousePosition;
        image.rectTransform.position = cursorPos;

        // if (!Application.isPlaying) {return;}

        // Cursor.visible = false;
    }
}
