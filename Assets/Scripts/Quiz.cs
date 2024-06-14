using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultButtonSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image  timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete = false;

    AudioSource audioSource;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;

        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            timer.loadNextQuestion = false;
            GoNextQuestion();
        }

        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    void GoNextQuestion()
    {
        if (questions.Count == 0)
        {
            Debug.Log("Questions are finished!");
        }
        else
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestions();
            scoreKeeper.IncrementQuestionSeen(); 
            progressBar.value ++;
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    void DisplayQuestions()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    public void OnAnswerSelected(int choice_index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(choice_index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

        if (progressBar.value == progressBar.maxValue)
        {
            isComplete = true;
        }
    }

    void DisplayAnswer(int choice_index)
    {
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        Image buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();

        if (choice_index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            questionText.text = "Sorry the correct answer was; \n" + currentQuestion.GetAnswer(correctAnswerIndex);
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponent<Button>().interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponent<Image>().sprite = defaultButtonSprite;
        }
    }
}
