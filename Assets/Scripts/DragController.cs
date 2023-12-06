using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    Vector3 mousePositonOffset;
    
    private Vector3 GetMouseWorldPositon()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnMouseDown()
    {
        mousePositonOffset = gameObject.transform.position - GetMouseWorldPositon();
    }

    private void OnMouseDrag()
    {
        if (GetComponent<BombBehavior>().isDraggable == true)
        {
            GetComponent<BombBehavior>().dragging = true;
            transform.position = GetMouseWorldPositon() + mousePositonOffset;
            //Debug.Log(GetComponent<BombBehavior>().dragging);
        }
    }

    private void OnMouseUp()
    {
        GetComponent<BombBehavior>().dragging = false;
    }
    
    
}
