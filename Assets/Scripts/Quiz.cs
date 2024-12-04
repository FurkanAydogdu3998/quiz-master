using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Question Area")]
    QuestionSO currentQuestion;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI questionText;
    
    [Header("Answer Area")]
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    bool isPlayerAnswered;
    
    [Header("Timer Area")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Progression Area")]
    [SerializeField] Slider progressBar;
    [SerializeField] bool isGameFinished = false;

    [Header("Score Keeper Area")]
    [SerializeField] GameObject scoreText;
    ScoreKeeper scoreKeeper;
    // Start is called before the first frame update
    
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
        GetNextQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        float fillFraction = timer.GetFillFraction();
        bool timerStateIsAnswering = timer.GetIsAnsweringQuestion(); 
        int correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();

        timerImage.fillAmount = fillFraction;

        if (timer.GetLoadNextQuestion() && questions.Count != 0) {
            GetNextQuestion();
            timer.SetLoadNextQuestion(false);
        } else if (!isPlayerAnswered && !timerStateIsAnswering) {
            SetButtonState(false);
            SelectAnswerWithMessage(correctAnswerIndex, "Time runned out, Correct Answer was: " + currentQuestion.GetAnswer(correctAnswerIndex));
            if (questions.Count == 0) {
                timer.StopTimer();
            }
            updateScoreText();
            FinishGame();
        } else if (questions.Count == 0 && !timerStateIsAnswering) {
            timer.StopTimer();
        }
    }

    public bool GetIsGameFinished() {
        return isGameFinished;
    }

    private void FinishGame() {
        if (progressBar.value == progressBar.maxValue) {
            isGameFinished = true;
        }
    }

    private void GetRandomQuestion() {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion)) {
            questions.Remove(currentQuestion);
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
        GetRandomQuestion();
        DisplayQuestion();
        isPlayerAnswered = false;
        scoreKeeper.IncrementQuestionsSeen();
        progressBar.value++;
    }

    private void DisplayQuestion() {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++) {
            TextMeshProUGUI buttonTextComponent = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            string answerForThisButton = currentQuestion.GetAnswer(i);

            buttonTextComponent.text = answerForThisButton;
        }
    }

    public void OnAnswerSelected(int index) {
        string text;
        if (index == currentQuestion.GetCorrectAnswerIndex()) {
            text = "Congratulations!";
            scoreKeeper.IncreaseCorrectAnswers();
        } else {
            text = "OOOOH NO Correct Answer was: " + currentQuestion.GetAnswer(currentQuestion.GetCorrectAnswerIndex());
        }

        SelectAnswerWithMessage(index, text);
        isPlayerAnswered = true;
        SetButtonState(false);
        timer.CancelTimer();
        updateScoreText();
        FinishGame();
    }

    private void updateScoreText() {
        TextMeshProUGUI scoreTextComponent = scoreText.GetComponentInChildren<TextMeshProUGUI>();
        scoreTextComponent.text = "Score: " + scoreKeeper.CalculateCurrentScore() + "%";
    }
}
