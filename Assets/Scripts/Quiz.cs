using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionSO question;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    // Start is called before the first frame update
    void Start()
    {
        GetNextQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetButtonSpritesToDefault() {
        for(int i = 0; i < answerButtons.Length; i++) {
            Image imageComponent = answerButtons[i].GetComponent<Image>();
            imageComponent.sprite = defaultAnswerSprite;
        }
    }

    private void SetButtonState(bool state) {
        for (int i = 0; i < answerButtons.Length; i++) {
            Button buttonComponent = answerButtons[i].GetComponent<Button>();
            buttonComponent.interactable = state;
        }
    }

    private void GetNextQuestion() {
        SetButtonState(true);
        SetButtonSpritesToDefault();
        DisplayQuestion();
    }

    private void DisplayQuestion() {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++) {
            TextMeshProUGUI buttonTextComponent = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            string answerForThisButton = question.GetAnswer(i);

            buttonTextComponent.text = answerForThisButton;
        }
    }

    public void OnAnswerSelected(int index) {
        if (index == question.GetCorrectAnswerIndex()) {
            Image selectedButtonImage = answerButtons[index].GetComponent<Image>();
            selectedButtonImage.sprite = correctAnswerSprite;

            questionText.text = "Congratulations!";
        } else {
            Image correctButtonImage = answerButtons[question.GetCorrectAnswerIndex()].GetComponent<Image>();
            correctButtonImage.sprite = correctAnswerSprite;

            questionText.text = "OOOOH NO Correct Answer is: " + question.GetAnswer(question.GetCorrectAnswerIndex());
        }

        SetButtonState(false);
    }
}
