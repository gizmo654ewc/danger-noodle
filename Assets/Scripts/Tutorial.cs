using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    /*[SerializeField] private float delayedLoading = 8f;
    private float elapse;

    public void Update()
    {
        elapse += Time.deltaTime;

        if (elapse > delayedLoading)
        {
            SceneManager.LoadSceneAsync(2);
        }
    }*/

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);
    }
}