﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
#pragma warning disable 649
    //ref to the QuizGameUI script
    [SerializeField] private QuizGameUI quizGameUI;
    //ref to the scriptableobject file
    [SerializeField] private List<QuizDataScriptable> quizDataList;
    [SerializeField] private float timeInSeconds;

    public static bool gameHasEnded = false;

#pragma warning restore 649

    private string currentCategory = "";
    private int correctAnswerCount = 0;
    //questions data
    private List<Question> questions;
    //current question data
    private Question selectedQuestion = new Question();
    public int gameScore;
    public int lifesRemaining;
    private float currentTime;
    private QuizDataScriptable dataScriptable;

    private GameStatus gameStatus = GameStatus.NEXT;

    public GameStatus GameStatus { get { return gameStatus; } }

    public List<QuizDataScriptable> QuizData { get => quizDataList; }
    public TextMeshProUGUI gameModeText;

    public void StartGame(int categoryIndex, string category)
    {
        currentCategory = category;
        correctAnswerCount = 0;
        gameScore = 0;
        lifesRemaining = 3;
        currentTime = timeInSeconds;
        //set the questions data
        questions = new List<Question>();
        dataScriptable = quizDataList[categoryIndex];
        questions.AddRange(dataScriptable.questions);
        //select the question
        SelectQuestion();
        gameStatus = GameStatus.PLAYING;
    }

    /// <summary>
    /// Method used to randomly select the question form questions data
    /// </summary>
    private void SelectQuestion()
    {
        //get the random number
        int val = UnityEngine.Random.Range(0, questions.Count);
        //set the selectedQuestion
        selectedQuestion = questions[val];
        //send the question to quizGameUI
        quizGameUI.SetQuestion(selectedQuestion);

        questions.RemoveAt(val);
    }

    private void Update()
    {
        if (gameStatus == GameStatus.PLAYING)
        {
            currentTime -= Time.deltaTime;
            SetTime(currentTime);
        }
    }

    void SetTime(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);                       // set the time value
        quizGameUI.TimerText.text = time.ToString("mm':'ss");   // convert time to Time format

        if (currentTime <= 0)
        {
            // Game Over
            GameEnd();
        }
    }

    // Method called to check the answer is correct or not
 
    public bool Answer(string selectedOption)
    {
        bool gameMode = bool.Parse(gameModeText.text);
        //set default to false
        bool correct = false;
        //if selected answer is similar to the correctAns
        if (gameMode) {
            if ((selectedQuestion.correctAns == selectedOption))
            {
                //Yes, Ans is correct
                correctAnswerCount++;
                correct = true;
                gameScore += 5;
                quizGameUI.ScoreText.text = gameScore.ToString();
            }
            else
            {
                //No, Ans is wrong
                //Reduce Life
                lifesRemaining--;
                quizGameUI.ReduceLife(lifesRemaining);
                if(gameScore >= 5)
                {
                    gameScore -= 5;
                    quizGameUI.ScoreText.text = gameScore.ToString();
                }

                if (lifesRemaining == 0)
                {
                    GameEnd();
                }
            }
        }
        else
        {
            if ((selectedQuestion.correctAns != selectedOption))
            {
                //Yes, Ans is correct
                correctAnswerCount++;
                correct = true;
                gameScore += 5;
                quizGameUI.ScoreText.text = gameScore.ToString();
            }
            else
            {
                //No, Ans is wrong
                //Reduce Life
                lifesRemaining--;
                quizGameUI.ReduceLife(lifesRemaining);
                if (gameScore >= 5)
                {
                    gameScore -= 5;
                    quizGameUI.ScoreText.text = gameScore.ToString();
                }

                if (lifesRemaining == 0)
                {
                    GameEnd();
                }
            }
        }


        if (gameStatus == GameStatus.PLAYING)
        {
            if (questions.Count > 0)
            {
                //call SelectQuestion method again after 1s
                Invoke("SelectQuestion", 0.4f);
            }
            else
            {
                GameEnd();
            }
        }
        //return the value of correct bool
        return correct;
    }

    public void GameEnd()
    {
        gameStatus = GameStatus.NEXT;
        quizGameUI.GameOverPanel.SetActive(true);

        //fi you want to save only the highest score then compare the current score with saved score and if more save the new score
        //eg:- if correctAnswerCount > PlayerPrefs.GetInt(currentCategory) then call below line

        //Save the score
        PlayerPrefs.SetInt(currentCategory, correctAnswerCount); //save the score for this category
    }
}

//Datastructure for storeing the quetions data
[System.Serializable]
public class Question
{
    public string questionInfo;         //question text
    public QuestionType questionType;   //type
    public Sprite questionImage;        //image for Image Type
    public List<string> options;        //options to select
    public string correctAns;           //correct option
}

[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE
}

[SerializeField]
public enum GameStatus
{
    PLAYING,
    NEXT
}