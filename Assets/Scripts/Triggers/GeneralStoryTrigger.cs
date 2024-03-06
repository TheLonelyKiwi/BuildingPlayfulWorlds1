using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//summary: uses UnityEvents to trigger story events
//Daan Meijneken, 20/12/2023, BPW1
public class GeneralStoryTrigger : MonoBehaviour
{
    public UnityEvent onTrigger;
    public UnityEvent OnSpecialTrigger;
    public UnityEvent onExit;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onTrigger.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onExit.Invoke();
        }
    }
}
