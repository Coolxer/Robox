using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingManager : MonoBehaviour
{
    private bool dragging = false;

    void OnMouseDown()
    {
        dragging = true;
    }

    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if(dragging)
                dragging = false;
        }
    }

    public bool isDragging()
    {
        return dragging;
    }
}
