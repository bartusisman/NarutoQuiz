using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")] 
public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)] //helps us to determine the text area of the serialized field as (min,max) lines
    [SerializeField] string question = "Enter new question text here!";
    [SerializeField] string[] Answers = new string[4];
    [SerializeField] int CorrectAnswerIndex; 

    public string GetQuestion()
    {
        return question;
    }

    public string GetAnswer(int index)
    {
        return Answers[index];
    }

    public int GetCorrectAnswerIndex()
    {
        return CorrectAnswerIndex;
    }

}
