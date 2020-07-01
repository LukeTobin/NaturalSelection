using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeManager : MonoBehaviour
{


    //Declaring variables
    private int dayLengthSecs;
    private int currentTime;
    private bool isRunning = true;
    private bool dayOver = false;
    public bool clockPaused = false;
    public Image night;

    [Header("The day length in seconds")]
    public int dayLengthMins;

    private bool nightshift;
    private float alphaAmount = 0f;
    private float alphaIncrement = 0f;

    [Header("The max darkness (pure darkness = 1.0f)")]
    public float alphaMax = 0.8f;

    [Header("Temperature increments per second")]
    public float morningTemp = 1f;
    public float noonTemp = 2f;
    public float eveningTemp = 1f;
    public float nightTemp = 2f;

    [Header("Time of day booleans (NO TOUCHY!)")]
    public bool isMorning = false;
    public bool isNoon = false;
    public bool isEvening = false;
    public bool isNight = false;

    private ScoreSystem scores;
    public Text ScoreText;

    //Starts the clock when it is enabled (at the start of the game)
    //OnEnable() is used here in case the game gets paused or the clock needs to be stopped at some point
    void OnEnable()
    {
        scores = GameObject.FindGameObjectWithTag("score").GetComponent<ScoreSystem>();
        //Is used to declare how long the day lasts (you can change the value in inspector (*2))
        dayLengthSecs = dayLengthMins * 2;
        //Is used to say how much the alpha needs to be incremented with depending on the length of the day
        alphaIncrement = alphaMax / dayLengthSecs;
        EnableGameClock();
    }

    void OnDisable()
    {
        StopGameClock();
    }

    private void Start()
    {
        isNoon = true;
        //scores = GameObject.FindGameObjectWithTag("score").GetComponent<ScoreSystem>();
    }

    void Update()
    {
        //ScoreText.text = "Score: " + scores.score++;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (clockPaused)
            {
                ResumeGameClock();
            }
            else
            {
                PauseGameClock();
            }
        }

        /*
        if(currentTime >= 10 && !nightshift)
        {           
            night.color = new Color(night.color.r, night.color.g, night.color.b, 0.7f);
            nightshift = true;
        }
        else if(nightshift && currentTime < 10)
        {
            night.color = new Color(night.color.r, night.color.g, night.color.b, 0f);
            nightshift = false;
        }*/
    }

    //This is the clock used for the day and night cycle
    private IEnumerator GameClock()
    {
        //This checks to see if the clock is running, in other words not disabled or paused
        while (isRunning)
        {

            //This checks to see if the game is paused, if not, the clock will return nothing and do nothing
            if (clockPaused)
            {
                yield return null;
            }
            else
            {
                currentTime++;
                ScoreText.text = "Score: " + scores.score++;

                //This here looks to see if the day is at it's very start, if so, the image alpha will be 0
                if (currentTime == 0)
                {
                    alphaAmount = 0;
                    night.color = new Color(night.color.r, night.color.g, night.color.b, alphaAmount);
                }
                //If it's not the start of the day and the day is on its first half, the alpha will increase by the alpha increment
                else if (currentTime <= dayLengthSecs / 2)
                {
                    alphaAmount += alphaIncrement;
                    night.color = new Color(night.color.r, night.color.g, night.color.b, alphaAmount);
                }
                //If it's not the start of the day and the day is on its second half, the alpha will decrease by the alpha increment
                else
                {
                    alphaAmount -= alphaIncrement;
                    night.color = new Color(night.color.r, night.color.g, night.color.b, alphaAmount);
                }


                //This checks to see if the game time is greater than the day length in seconds, which means the day is over and a new day starts
                if (currentTime >= dayLengthSecs)
                {
                    dayOver = true;
                    ResetGameClock();
                }

                if (currentTime == 0)
                {
                    isNoon = true;
                    isMorning = false;
                    //scores.score += 25;
                }
                else if (currentTime == (Mathf.Round(dayLengthSecs/4)))
                {
                    isEvening = true;
                    isNoon = false;
                    //scores.score += 25;
                }
                else if (currentTime == (dayLengthSecs/2))
                {
                    isNight = true;
                    isEvening = false;
                    //scores.score += 25;
                }
                else if (currentTime == (Mathf.Round(dayLengthSecs/4*3)))
                {
                    isMorning = true;
                    isNight = false;
                    //scores.score += 100;
                }

                //If the day is still going, this enumerator function will run again after 1 second. This saves resources over using update, which goes every frame
                yield return new WaitForSeconds(1f);
            }


        }
    }

    //This will pause the in game clock. I'll leave this here in case we ever need it, like a pause game option for example
    private void PauseGameClock()
    {
        Debug.LogWarning("Game Clock is now paused :)");
        clockPaused = true;
    }

    //This will resume the game clock if it's paused
    private void ResumeGameClock()
    {
        Debug.LogWarning("Game Clock will now resume :)");
        clockPaused = false;
    }

    //This will reset the game clock when the day is over, so a new one will start
    private void ResetGameClock()
    {
        Debug.Log("The day is now over :)");
        currentTime = 0;
        dayOver = false;
    }

    //If you are in the start menu or whenever it's useful, you can use this method to stop the game clock so it won't bog down the game
    public void StopGameClock()
    {
        Debug.LogWarning("The game clock has been stopped :)");
        isRunning = false;
        clockPaused = false;
        currentTime = 0;
    }

    private void EnableGameClock()
    {
        clockPaused = false;
        isRunning = true;
        StartCoroutine(GameClock());
    }

}
