using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour

{
    float currentTime = 0f;
    float startingTime = 15f;

    float timeReset = 15f;

    [SerializeField] TextMeshProUGUI countdownText;

    public QuizManagers quizmanager;

    public bool powerUpStatus = false;

    public float powerUpDuration = 5f;

    public float powerUpMultiplier = 1f;

    public float powerUpTimer; 


//    public string timerText;

    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (powerUpStatus == true)
        {
            powerUpTimer -= Time.deltaTime;
            if(powerUpTimer <= 0f)
            {
                powerUpStatus = false;
                powerUpMultiplier = 1f;
            } else
            {
                powerUpMultiplier = 0.4f;
            }

        } else
        {
            powerUpMultiplier = 1f;
        }

        currentTime -= Time.deltaTime * powerUpMultiplier;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            quizmanager.correct();

        }


    }

    public void ResetTheTimerOnNewQuestion()
    {
        currentTime = startingTime;
    }

    public void ActivePowerUpTime()
    {
        powerUpStatus = true;
        powerUpTimer = powerUpDuration;
    }

}
