using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionSO question;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] answerButtons;
    // Start is called before the first frame update
    void Start()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++) {
            TextMeshProUGUI buttonTextComponent = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            string answerForThisButton = question.GetAnswer(i);

            buttonTextComponent.text = answerForThisButton;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
