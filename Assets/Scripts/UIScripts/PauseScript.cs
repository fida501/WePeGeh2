using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{

    public GameObject pauseMenu;

    public bool isPaused;

    public GameObject soundsettingMenu;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Inside this ");
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        //Debug.Log("Inside Pausegame");
        pauseMenu?.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu?.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void SettingsMenu()
    {

        soundsettingMenu.SetActive(true);
        pauseMenu.SetActive(false);

    }

    public void SettingsPower()
    {
        SceneManager.LoadScene("GameStartScene");
    }

    public void backToPause()
    {
        pauseMenu.SetActive(true);
        soundsettingMenu.SetActive(false);
    }
}
