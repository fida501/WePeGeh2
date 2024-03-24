using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject test;
    public TextMeshProUGUI CurrentScoreText;
    private int scoreNum;
    // Start is called before the first frame update
    void Start()
    {
        scoreNum = 0;
        CurrentScoreText.text = "Score : " + scoreNum;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
