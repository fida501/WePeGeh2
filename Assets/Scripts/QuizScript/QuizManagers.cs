using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEditor;
using JetBrains.Annotations;

public class QuizManagers : MonoBehaviour
{

    public QuestionAndAnswersHolder qnaHolder;
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    //Quiz FIle Location
    public TextAsset questionFileXml;


    public string questionFilePath;
    public TextMeshProUGUI QuestionText;
    public string targetScene = "MoveSceneAfterQuiz";
    public int currentBabQuiz;

    [Header("Timer")]
    public GameObject timerFunction;
    public CountdownTimer countdownTimer;


    //Score Variable
    [Header("Scoring")]
    public string ScoreText;
    public int scoreNum;
    public TextMeshProUGUI scoreTextMesh;

    [Header("Power Up")]
    public GameObject ButtonPowerUpErase;
    public bool powerUpErase = false;
    public GameObject ButtonPowerUpHeart;
    public bool powerUpHeart = false;
    public GameObject ButtonPowerUpTime;
    //public bool powerUpTime = false;

    [Header("PrePlayUI")]
    public GameObject prePlayObject;

    private void Start()
    {
        prePlayObject.SetActive(true);
        LoadXMLFile();
        generateQuestion();
        scoreNum = 0;
        ScoreText = "Score : " + scoreNum;
        scoreTextMesh.text = ScoreText;

        currentBabQuiz = PlayerPrefs.GetInt("CurrentPlayerBabSoal");
        questionFilePath = "SoalBab" + currentBabQuiz.ToString();

        questionFileXml = Resources.Load<TextAsset>(questionFilePath);

    }

    void Update()
    {

        ScoreText = "Score : " + scoreNum;
        scoreTextMesh.text = ScoreText;

    }

    public void correct()
    {

        QnA.RemoveAt(currentQuestion);
        ActivingButtons();
        generateQuestion();
        countdownTimer.ResetTheTimerOnNewQuestion();
        Debug.Log("Inside the correct");

    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswers == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);

            QuestionText.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else
        {
            Debug.Log("Out of Questions");
            QuestionText.text = "Sudah tidak ada pertanyaan";
            currentBabQuiz = currentBabQuiz + 1;
            tunggu3Detik();
            PlayerPrefs.SetInt("CurrentPlayerBabSoal", currentBabQuiz);
            SceneManager.LoadScene(targetScene);

        }

    }

    public void LoadXMLFile()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(QuestionAndAnswersHolder));
        using (StringReader reader = new StringReader(questionFileXml.text))
        {

            Debug.Log("Inside load xml");
            qnaHolder = (QuestionAndAnswersHolder)serializer.Deserialize(reader);
            QnA = qnaHolder.QnA;


        }

    }

    public void UpdateScoreOnQuizCorrect()
    {
        scoreNum = scoreNum + 10;
    }

    async Task tunggu3Detik()
    {
        await Task.Delay(153000);
    }

    public void PowerUpErase()
    {
        Debug.Log("Inside powerup erase ");
        int randomEraseNumber1 = UnityEngine.Random.Range(0, 4);
        int randomEraseNumber2 = UnityEngine.Random.Range(0, 4);
        if (randomEraseNumber2 == randomEraseNumber1)
        {
            randomEraseNumber2 = UnityEngine.Random.Range(0, 4);
        }

        if (options[randomEraseNumber1].GetComponent<AnswerScript>().isCorrect == true)
        {
            randomEraseNumber1 = UnityEngine.Random.Range(0, 4);
        }

        if (options[randomEraseNumber2].GetComponent<AnswerScript>().isCorrect == true)
        {
            randomEraseNumber2 = UnityEngine.Random.Range(0, 4);
        }


        Debug.Log("the value of this number 1 are == " + randomEraseNumber1);
        Debug.Log("the value of this number 2 are == " + randomEraseNumber2);

        options[randomEraseNumber1].gameObject.SetActive(false);
        //options[randomEraseNumber2].gameObject.SetActive(false);



        powerUpErase = true;
        ButtonPowerUpErase.SetActive(false);
    }

    public void PowerUpHeart()
    {

        Debug.Log("Inside powerup Heart ");
        powerUpHeart = true;
        ButtonPowerUpHeart.SetActive(false);
    }

    public void PowerUpTime()
    {

        Debug.Log("Inside powerup Time ");
        countdownTimer.ActivePowerUpTime();
        ButtonPowerUpTime.SetActive(false);

    }

    public void ActivingButtons()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].gameObject.SetActive(true);
        }
    }

    public void PlayTheQuiz()
    {
        prePlayObject.SetActive(false);
    }

}
