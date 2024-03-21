using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public int scoreCount;
    private GameObject scoreObj;
    ScoreHolder scoreScript;

    // Start is called before the first frame update
    void Start()
    {
        scoreObj = GameObject.FindWithTag("Score");
        scoreScript = scoreObj.GetComponent<ScoreHolder>();
    }

    private void OnTriggerEnter2D(Collider2D DeathPlane)
    {
        if(DeathPlane.tag == "KillPlane")
        {
            Debug.Log("did it");
            scoreScript.UpdateScore(scoreCount);
        }
    }
}
