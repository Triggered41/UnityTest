using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

// Timer Component
public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float timePassed = 0;
    public Vector2 time;
    private float timeInSec;
    private bool isTimerRunning = false;
    public UnityEvent onTimerEnd;
    // Start is called before the first frame update
    void Start()
    {
        // Store the time in seconds
        timeInSec = time.x*60 + time.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Run the timer untill time is 0
        if (isTimerRunning){
            if (timeInSec > 0){
                RunTimer();
            }else{
                time = Vector2.zero;
                isTimerRunning = false;
                onTimerEnd.Invoke();
            }
        }
        
    }

    // Run the timer
    void RunTimer(){
        timePassed += Time.fixedDeltaTime;
        timeInSec -= Time.fixedDeltaTime;
        time = new Vector2(Mathf.Floor(timeInSec/60), timeInSec%60);
    }

    // Start the timer
    public void StartTimer(){
        isTimerRunning = true;
    }

    // To start timer with custom time.
    public void StartTimer(float T){
        isTimerRunning = true;
        timeInSec = T;
    }
}
