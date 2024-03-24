using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUIScript : MonoBehaviour
{


    public void StartGame()
    {
        PlayerPrefs.SetInt("CurrentPlayerBabSoal", 1);
        SceneManager.LoadScene("MoveScene");
    }

    public void CloseGame()
    {
        // Exit the game
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }
}
