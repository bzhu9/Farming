using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    bool stopwatchActive = true;
    float currentTime;
    public Text currentTimeText;

    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopwatchActive == true) {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds (currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss");
    }
    public void StartStopwatch(){
        stopwatchActive = true;
    }
    public void StopStopwatch(){
        stopwatchActive = false;
    }
}
