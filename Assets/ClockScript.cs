using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ClockScript : MonoBehaviour
{
    public float timer = 0.0f;
    public int seconds;
    public int mins; 
    public Text timerText; 
    public string temp;

    void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= 60)
        {
            mins += 1;
            timer -= 60;
        }

        seconds = (int)timer;

        if (mins < 10)
        {
            if (seconds < 10) {
                temp = "0" + mins.ToString();
                temp += ":0" + seconds.ToString();
            } else {
                temp = "0" + mins.ToString();
                temp += ":" + seconds.ToString();
            } 
        } else 
        {
            if (seconds < 10) {
                temp = mins.ToString();
                temp += ":0" + seconds.ToString();
            } else {
                temp = mins.ToString();
                temp += ":" + seconds.ToString();
            } 
        }

        timerText.text = temp;
    }
}
