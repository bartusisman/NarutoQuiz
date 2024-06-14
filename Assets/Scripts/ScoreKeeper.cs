using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;

    public int GetCorrecAnswers()
    {
        return correctAnswers;
    }

    public int IncrementCorrectAnswers()
    {
        return correctAnswers++;
    }

    public int GetQuestionSeen()
    {
        return questionsSeen;
    }

    public int IncrementQuestionSeen()
    {
        return questionsSeen++;
    }

    public float CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }




}
