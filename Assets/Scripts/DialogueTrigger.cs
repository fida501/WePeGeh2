using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("INK Json")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    public GameObject NPCGameObject;

    public string CurrentNPCSpriteName;

    public SpriteRenderer spriteRenderer;

    public bool buMutiaTriggerAlr = false;

    private void Awake()
    {
        //Debug.Log("Awaken");
        playerInRange = false;
        visualCue.SetActive(false);
        //        SpriteRenderer spriteRenderer = NPCGameObject.GetComponent<SpriteRenderer>();
        spriteRenderer = NPCGameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        CurrentNPCSpriteName = GetSpriteName(spriteRenderer);
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Z))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);

            }

        }
        else
        {
            visualCue.SetActive(false);
        }

        if (playerInRange && CurrentNPCSpriteName == "BuMutia" && !buMutiaTriggerAlr)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            buMutiaTriggerAlr=true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    public void chosen(string chosen1)
    {
        Debug.Log("Value of chosen are == " + chosen1);
        if (chosen1 == "Selamat Belajar")
        {
            //Debug.Log("Didalam Sealamt Bealajar");
        }
    }

    public string GetSpriteName(SpriteRenderer spriterenderer)
    {
        Sprite sprite = spriterenderer.sprite;
        string spriteName = sprite.name;
        return spriteName;
    }
}
