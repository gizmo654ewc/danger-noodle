using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicRat_Anim : MonoBehaviour
{
    Rigidbody2D rb;
    Animator bRat_Anim;
    EnemyMovementScript eMove;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bRat_Anim = GetComponent<Animator>();
        eMove = GetComponent<EnemyMovementScript>(); 
    }

    // Update is called once per frame
    void Update()
    {
        /*
        eMove.ClimbTo();

        if (!climbing)
        {
            bRat_Anim.SetBool("isClimbing", true);
            Debug.Log("climbing");
        }
        else
        {
            bRat_Anim.SetBool("isClimbing", false);
            Debug.Log("nah");
        }
        */
    }
}
