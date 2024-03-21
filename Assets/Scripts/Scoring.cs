using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : TextMeshProUGUI
{

    public Text scoreText;
    public float scoreCount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + Mathf.Round(scoreCount);
    }
}
