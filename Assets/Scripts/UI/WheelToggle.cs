using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//summary: enables or disables the turret select window when player gives input.
//Daan Meijneken, 17/12/2023, BPW1
public class WheelToggle : MonoBehaviour
{
    private bool isActive = true;

    private void Start()
    {
        ToggleWheel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleWheel();
        } else if (Input.GetKeyUp(KeyCode.Tab))
        {
            ToggleWheel();
        }
    }
    
    private void ToggleWheel()
    {
        isActive = !isActive;
        
        //stolen from StackOverFlow (I could have done this myself but I was lazy)
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var child = gameObject.transform.GetChild(i).gameObject;
            if (child != null) child.SetActive(isActive);
        }
    }
}
