using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnim : MonoBehaviour
{
    PlayerMovement pMove;
    Rigidbody2D rb;
    private Animator Snake;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Snake = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        //Set yVelocity in animator
        //Snake.SetFloat("yVelo", rb.velocity.y);

        // Shoot bool
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Snake.SetBool("isShooting", true);
        }
        else
        {
            Snake.SetBool("isShooting", false);
        }
    }
}
