using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Question Area")]
    [SerializeField] QuestionSO question;
    [SerializeField] TextMeshProUGUI questionText;
    
    [Header("Answer Area")]
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    bool isPlayerAnswered;
    
    [Header("Timer Area")]
    [SerializeField] Image timerImage;
    Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        float fillFraction = timer.GetFillFraction();
        bool timerStateIsAnswering = timer.GetIsAnsweringQuestion(); 
        int correctAnswerIndex = question.GetCorrectAnswerIndex();

        timerImage.fillAmount = fillFraction;

        if (timer.GetLoadNextQuestion()) {
            GetNextQuestion();
            timer.SetLoadNextQuestion(false);
        } else if (!isPlayerAnswered && !timerStateIsAnswering) {
            SetButtonState(false);
            SelectAnswerWithMessage(correctAnswerIndex, "Time runned out, Correct Answer is: " + question.GetAnswer(correctAnswerIndex));
        }
    }

    private void SelectAnswerWithMessage(int index, string text) {
        questionText.text = text;

        Image selectedButtonImage = answerButtons[index].GetComponent<Image>();
        selectedButtonImage.sprite = correctAnswerSprite;
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
        isPlayerAnswered = false;
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
        string text;
        if (index == question.GetCorrectAnswerIndex()) {
            text = "Congratulations!";
        } else {
            text = "OOOOH NO Correct Answer is: " + question.GetAnswer(question.GetCorrectAnswerIndex());
        }

        SelectAnswerWithMessage(index, text);
        isPlayerAnswered = true;
        SetButtonState(false);
        timer.CancelTimer();
    }
}
