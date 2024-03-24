using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;

    public string characterName;

    public TextMeshProUGUI characterObjectName;

    public bool dialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    public string targetScene = "QuizScene";

    public int currentBabSoal = 1;

    //public QuizManagers quizManagers;

    //public GameObject[] objectsToDontDestroy;

    private void Awake()
    {
        characterObjectName.text = characterName;

        if (instance != null)
        {
            Debug.LogWarning("Error, more than 1 thing in this one");
        }
        instance = this;
 //       foreach (GameObject obj in objectsToDontDestroy)
 //       {
 //           DontDestroyOnLoad(this.gameObject);
//
//        }
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        Debug.Log("The value of thsi bab are == " + PlayerPrefs.GetInt("CurrentPlayerBabSoal"));

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {

        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        currentStory.BindExternalFunction("startQuiz", (string cobaString) =>
        {
            //quizManagers.currentBabQuiz = currentBabSoal;
            PlayerPrefs.SetInt("CurrentPlayerBabSoal", currentBabSoal);
            StartQuizScene();
        });

        currentStory.BindExternalFunction("giveSpeakerName", (string speakerName) =>
        {
            giveSpeakerName(speakerName);
        });

        ContinueStory();
    }



    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();

            //display choices
            DisplayChoices();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
            //           ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices bla bla ada error di display choice. Number of choices given : " + currentChoices.Count);
        }
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        currentStory.UnbindExternalFunction("startQuiz");
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

    }

    private IEnumerator SelectFirstChoice()
    {
        
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        //Debug.Log(choiceIndex);
        currentStory.ChooseChoiceIndex(choiceIndex);
        //currentStory.Continue();
        //ContinueStory();
    }

    public void StartQuizScene()
    {
        Debug.Log("Inside the start quiz scene");


        SceneManager.LoadScene(targetScene);
        StartCoroutine(ExitDialogueMode());
    }

    public void giveSpeakerName(string speakerName)
    {
        characterObjectName.text = speakerName;
    }
}
