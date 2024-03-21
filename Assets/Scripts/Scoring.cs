using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{

    public Text scoreText;
    public int scoreCount;
    private GameObject textRead;



    // Start is called before the first frame update
    void Start()
    {
        scoreText = textRead.GetComponent<Text>();
        scoreCount = 0;
        scoreText.text = "Score : " + scoreCount;
    }

    private void OnTriggerEnter2D(Collider2D DeathPlane)
    {
        if(DeathPlane.tag == "KillPlane")
        {
            scoreCount += 100;
            scoreText.text = "Score" + scoreCount;
        }
    }
}
