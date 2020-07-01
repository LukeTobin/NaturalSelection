using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{

    public int score = 0;
    public int daysSurvived = 0;
    private bool playerDied;
    public Text text;



    public void ToggleGOtext()
    {
        text.text = "YOU DIED\n\n\nThanks for playing!\n\nPress ENTER to try again!";
    }

    public void OnDeathScore()
    {
        Debug.Log("Your score is: " + score + " and you've survived " + daysSurvived + " days in total. Thanks for playing and have a nice death!");
        playerDied = true;
        ToggleGOtext();
    }

    public void Update()
    {
        if (playerDied && Input.GetKeyDown(KeyCode.Return))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
