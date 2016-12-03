using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    private Text timerLabel;

    private float time;

    void Start ()
    {
        timerLabel = this.GetComponent<Text>();
    }

    void Update()
    {
        time += Time.deltaTime;

        double minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
        double seconds = time % 60;//Use the euclidean division for the seconds

        //update the label value
        timerLabel.text = "Time : " + string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
