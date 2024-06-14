using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 15f;
    [SerializeField] float timeToShowCorrectAnswer = 5f;

    public bool loadNextQuestion;
    public float fillFraction;
    public bool isAnsweringQuestion = false;
    public bool isInQuestions = false;
    
    float timerValue;

    void Update()
    {
        if(isInQuestions)
        {
            UpdateTimer();
        }
        
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime; //reduces the time in every frame

        if(isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }

            else
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }

        else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }

            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
