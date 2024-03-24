using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnim : MonoBehaviour
{
    PlayerMovement pMove;
    Rigidbody2D rb;
    private Animator pAnim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pAnim = GetComponent<Animator>();
        pMove = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        pMove.IsGrounded();
        
        //Set yVelocity in animator
        pAnim.SetFloat("yVelo", rb.velocity.y);

        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            pAnim.SetBool("isShooting", true);
        }
        else
        {
            pAnim.SetBool("isShooting", false);
        }
        
       /* 
        if (Input.GetKeyDown(KeyCode.Z) && Input.GetButtonDown("Jump") && pMove.IsGrounded() && rb.velocity.y == 0)
        {
            pAnim.SetBool("isJShot", true);
            Debug.Log("jShot");
        }
        else
        {
            pAnim.SetBool("isJShot", false);
        }
        */
    }
}
