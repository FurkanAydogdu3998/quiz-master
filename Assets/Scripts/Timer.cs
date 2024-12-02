using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float timeToAnswerTheQuestion = 30f;
    [SerializeField] float timeToShowingCorrectAnswer = 10f;
    float timerValue;
    bool isAnsweringQuestion = true;
    float fillFraction;
    public bool loadNextQuestion;
    bool shouldTimerStop = false;
    void Start()
    {
        timerValue = timeToAnswerTheQuestion;
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldTimerStop) {
            UpdateTimer();
        }
    }

    public void StopTimer() {
        shouldTimerStop = true;
    }

    public void SetLoadNextQuestion(bool state) {
        loadNextQuestion = state;
    }

    public bool GetLoadNextQuestion() {
        return loadNextQuestion;
    }

    public void CancelTimer() {
        timerValue = 0;
    }

    public float GetFillFraction() {
        return fillFraction;
    }

    private void UpdateTimer() {
        timerValue = timerValue - Time.deltaTime;

        if (timerValue <= 0) {
            if (isAnsweringQuestion) {
                timerValue = timeToShowingCorrectAnswer;
                isAnsweringQuestion = false;
            } else {
                timerValue = timeToAnswerTheQuestion;
                isAnsweringQuestion = true;
                loadNextQuestion = true;
            }
        } else {
            if (isAnsweringQuestion) {
                fillFraction = timerValue / timeToAnswerTheQuestion;
            } else {
                fillFraction = timerValue / timeToShowingCorrectAnswer;
            }
        }

        Debug.Log("Timer Value: " + timerValue + ", Fill Fraction: " + fillFraction);
    }

    public bool GetIsAnsweringQuestion() {
        return isAnsweringQuestion;
    }
}
