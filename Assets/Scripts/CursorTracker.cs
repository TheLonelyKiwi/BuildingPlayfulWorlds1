using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTracker : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    
    private Vector3 mousePos;
    private Vector3 hitPos;

    void Update()
    {
        //shoots ray from camera cursor to world cursor. 
        mousePos = Input.mousePosition;
        mousePos.z = 10;
        
        hitPos = Camera.main.ScreenToWorldPoint(mousePos);
        hitPos.z = 0;
    }

    public Vector3 GetMousePos()
    {
        return hitPos;
    }
}
