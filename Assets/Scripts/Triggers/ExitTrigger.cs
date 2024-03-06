using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitTrigger : MonoBehaviour
{
    private bool timerOver = false;
    public UnityEvent onTimerOver;

    public void SetActive(bool newState)
    {
        timerOver = newState;
        Debug.Log("Gate has been opened");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && timerOver)
        {
            onTimerOver.Invoke();
        }
    }
    
    
}
