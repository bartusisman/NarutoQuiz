using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndScript endScreen;
    WelcomeScript welcomeScript;
    Timer timer;


    void Start()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScript>();
        welcomeScript = FindObjectOfType<WelcomeScript>();
        timer = FindObjectOfType<Timer>();

        welcomeScript.gameObject.SetActive(true);
        quiz.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(false);

    }

    void Update()
    {
        if(quiz.isComplete)
        {
            welcomeScript.gameObject.SetActive(false);
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            timer.isInQuestions = false;
        }
    }

    public void onStartClick()
    {
        welcomeScript.gameObject.SetActive(false);
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
        timer.isInQuestions = true;
    }

    public void onExitClick()
    {
        // If running in the editor, stop playing the scene
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Quit the application
        Application.Quit();
        #endif
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
