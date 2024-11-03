using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LogicScript : MonoBehaviour
{
    public float timer = 60f;
    public Text timerText;
    private bool timerIsRunning = false;
    public Image scoreCard;
    public Image star1;
    public Image star2;
    public Image star3;
    public Text finalScore;
    public int playerScore;
    public Text scoreText;


    void Start()
    {
        // Start the timer
        timerIsRunning = true;
        scoreCard.enabled = false;
        star1.enabled = false;
        star2.enabled = false;
        star3.enabled = false;
        finalScore.enabled = false;

    }

    public void addScore()
    {
        playerScore += 10;
        scoreText.text = playerScore.ToString();
    }

    void levelComplete()
    {
        Time.timeScale = 0;
        scoreCard.enabled = true;
        finalScore.text = playerScore.ToString();
        finalScore.enabled = true;
        if (playerScore > 60)
        {
            star1.enabled = true;
        }
        if (playerScore > 120)
        {
            star2.enabled = true;
        }
        if (playerScore > 130)
        {
            star3.enabled = true;
        }
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timer > 0)
            {
                // Reduce time
                timer -= Time.deltaTime;
                // Update the timer UI
                DisplayTime(timer);
            }
            else
            {
                // Time has run out
                timer = 0;
                timerIsRunning = false;
                levelComplete();


                // Optionally, add code here to trigger an event or action when time runs out
            }
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        // Display time in minutes and seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
