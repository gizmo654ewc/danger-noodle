using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{

    public Text scoreText;
    public int scoreCount;


    // Start is called before the first frame update
    void Start()
    {
        scoreCount = 0;
        scoreText.text = "Score: " + scoreCount;

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D DeathPlane)
    {
        if (DeathPlane.tag == "KillPlane")
        {
            scoreCount += 100;
            scoreText.text = "Score: " + scoreCount;
        }
        Debug.Log("Scored!");
    }
}
