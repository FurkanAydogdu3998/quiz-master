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
    void Start()
    {
        timerValue = timeToAnswerTheQuestion;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
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
            }
        } else {
            if (isAnsweringQuestion) {
                fillFraction = timerValue / timeToShowingCorrectAnswer;
            } else {
                fillFraction = timerValue / timeToAnswerTheQuestion;
            }
        }

        Debug.Log(timerValue);
    }

    public bool GetIsAnsweringQuestion() {
        return isAnsweringQuestion;
    }
}
