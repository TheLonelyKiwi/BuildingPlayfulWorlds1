using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;


public class ObjectiveTimer : MonoBehaviour
{
    [SerializeField] private float timeToComplete = 60f;
    public UnityEvent OnTimerEnd;

    private bool timerOver = false; 
    private TextMeshProUGUI text;
    
    
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (timeToComplete > 0)
        {
            timeToComplete -= Time.deltaTime;
            DisplayTimer();
        }
        else if (!timerOver)
        {
            timerOver = true;
            OnTimerEnd?.Invoke();
            
            timeToComplete = 0;
            text.fontSize = 25;
            text.text = "Emergency Exit open. Get to the exit!";
        }
    }

    private void DisplayTimer()
    {
        //stole this from stackoflow
        int minutes = Mathf.FloorToInt(timeToComplete / 60);
        int seconds = Mathf.FloorToInt(timeToComplete % 60);
        text.text = string.Format("Survive for: " + "{0:00}:{1:00}", minutes, seconds); 
    }
}
