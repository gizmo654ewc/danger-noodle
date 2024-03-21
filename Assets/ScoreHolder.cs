using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreHolder : MonoBehaviour
{
    public int score = 000;
    private Text textComp;


    // Start is called before the first frame update
    void Start()
    {
        textComp = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textComp.text = "Score: " + score;
    }

    public void UpdateScore(int addedScore) 
    { 
        score += addedScore;
    }
}
