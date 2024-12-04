using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndGame endGame;
    
    // Start is called before the first frame update
    void Start()
    {
        endGame = FindObjectOfType<EndGame>();
        quiz = FindObjectOfType<Quiz>();
        quiz.gameObject.SetActive(true);
        endGame.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool isGameFinished = quiz.GetIsGameFinished();

        if (isGameFinished) {
            quiz.gameObject.SetActive(false);
            endGame.gameObject.SetActive(true);

            endGame.DisplayScoreText();
        }
    }

    public void PlayAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
