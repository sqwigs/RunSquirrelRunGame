using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Timer {

    private double timeLeft;
    private double timeLimit;

    /// <summary>
    /// Gets or Sets the total time left in this timer. 
    /// </summary>
    public double TimeLeft
    {
        get { return timeLeft;  } 
        set { timeLeft = value; }
    }

   public Timer (double _timeLimit)
    {
        this.timeLimit = _timeLimit;
        this.timeLeft = _timeLimit;
    }

    public void UpdateTimer()
    {
        timeLeft -= Time.deltaTime;
    }

    public void ResetTimer()
    {
        timeLeft = timeLimit;
    }

    public override string ToString()
    {
        double minutes = timeLeft / 60; //Divide the guiTime by sixty to get the minutes.
        double seconds = timeLeft % 60;//Use the euclidean division for the seconds

        return string.Format("{0:00} : {1:00}", Math.Floor(minutes), Math.Floor(seconds));
    }
}
