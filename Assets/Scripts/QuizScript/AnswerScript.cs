using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{

    public bool isCorrect = false;
    public QuizManagers quizManager;
    public void Answer()
    {


        if (isCorrect)
        {
            Debug.Log("Correct Answer");
            quizManager.UpdateScoreOnQuizCorrect();
            quizManager.correct();

        }
        else
        {
            Debug.Log("Wrong ANswer");
            if (quizManager.powerUpHeart == true)
            {
                Debug.Log("Maish ada satu kesempatan lagi !");
                this.gameObject.SetActive(false);
                quizManager.powerUpHeart = false;
            }
            else
            {
                quizManager.correct();
            }
        }
    }
}
