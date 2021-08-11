using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeedGame : MonoBehaviour
{
    public Text leftOrRight;
    public Text scoreDisplay;
    public int maxScore = 10;
    public float timeForAnswer = 1f;
    public GameObject gamePanel;

    private int score = 0;
    private static string choice = "none";
    private bool miss = false;
    private bool gameOver = false;
    private static bool clickedButton = false;
    private string leftRight = "left"; // false is left, true is right

    float timePassed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        setBlank();
        setLeftOrRight();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // show left or right
        // check if user input is correct
        if(gameOver == false)
        {
            
            startGame();
        }
        else
        {
            SceneManager.LoadScene("MuseumRoom");
        }

        scoreDisplay.text = score.ToString();
    }

    public void setLeftOrRight()
    {
        float targetButtonNum = Random.Range(0f, 1f);
        if(targetButtonNum <= 0.5f)
        {
            leftRight = "left";
            leftOrRight.text = "L";
        }
        else
        {
            leftRight = "right";
            leftOrRight.text = "R";

        }
    }
    public void setBlank()
    {
        leftOrRight.text = " ";
    }

    public static void leftClick()
    {
        choice = "left";
        clickedButton = true;
    }
    public static void rightClick()
    {
        choice = "right";
        clickedButton = true;
    }
    public void checkAnswer()
    {
        if(leftRight == choice)
        {
            miss = false;
        }
        else
        {
            miss = true;
        }

        if (miss)
        {
            score -= 2;
        }
        else
        {
            score++;
        }
    }

    public void checkScore()
    {
        if(score >= maxScore)
        {
            //load scene
            //increase key count
            gameOver = true;
        }
        if(score < -10)
        {
            // lose a life
            gameOver = true;
        }
    }

    private void startGame()
    {
        clickedButton = false;
        miss = false;
        
        if(clickedButton == false)
        {
            timePassed += Time.deltaTime;
            if(timePassed > timeForAnswer)
            {
                timePassed = 0.0f;
                miss = true;
                checkAnswer();
                leftRight = "none";
                choice = "none";
                setLeftOrRight();
            }
        }

        if (clickedButton)
        {
            timePassed = 0.0f;
            checkAnswer();
            leftRight = "none";
            choice = "none";
            setLeftOrRight();
        }
        checkScore();
    } 
}
