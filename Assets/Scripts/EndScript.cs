using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    private ScoreKeeper scoreKeeper;

    [SerializeField] private Image displayImage; // This is the UI Image component that will display the sprites
    [SerializeField] private Sprite[] imagesArr = new Sprite[3];
    [SerializeField] private AudioClip[] audioList = new AudioClip[3];

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

        float score = scoreKeeper.CalculateScore();
        DisplayResult(score);
    }

    private void DisplayResult(float score)
    {
        if (score >= 85)
        {
            finalScoreText.text = $"Score: {score}% Arigatou gozaimasu Hokage-Sama!!!";
            TriggerSituation(2);
        }
        else if (score >= 55)
        {
            finalScoreText.text = $"Score: {score}% You might be an imposter??!!";
            TriggerSituation(1);
        }
        else
        {
            finalScoreText.text = $"Score: {score}% Konohamaru-chan you got much more to learn from Naruto-niichan!!!";
            TriggerSituation(0);
        }
    }

    public void TriggerSituation(int index)
    {
        if (index < 0 || index >= audioList.Length || index >= imagesArr.Length)
        {
            Debug.LogWarning("Invalid index");
            return;
        }

        PlaySound(index);
        ShowImage(index);
    }

    private void PlaySound(int index)
    {
        audioSource.clip = audioList[index];
        audioSource.Play();
    }

    private void ShowImage(int index)
    {
        displayImage.sprite = imagesArr[index];
    }
}
