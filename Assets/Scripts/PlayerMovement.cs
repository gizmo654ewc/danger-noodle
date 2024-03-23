using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float hInput;
    private float vInput;
    private bool facingRight = true;
    [SerializeField] private float jump;
    [SerializeField] private float speed;
    [SerializeField] private float fireWait;
    private float currWait = 0;

    Rigidbody2D rb;
    SpriteRenderer p_SpriteRenderer;
    private Animator pAnim;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private GameObject emitter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pAnim = GetComponent<Animator>();
        p_SpriteRenderer = GetComponent<SpriteRenderer>();
        currWait = fireWait;
    }

    // Update is called once per frame
    void Update()
    {
        if (currWait > 0)
        {
            currWait -= Time.deltaTime;
        }

        FacingRight();
        // Required making horizontalInput float to make movement animation
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        pAnim.SetFloat("moveSpeed", Mathf.Abs(horizontalInput));

        if (facingRight)
        {
            emitter.transform.position = transform.position + new Vector3(.85f, 0, 0);
        }
        else
        {
            emitter.transform.position = transform.position + new Vector3(-.85f, 0, 0);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded() && rb.velocity.y == 0)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            pAnim.SetTrigger("Jumping");
        }
        
        // Animation grounded boolean
        if (IsGrounded())
        {
            pAnim.SetBool("isGround", true);
        }
        else
        {
            pAnim.SetBool("isGround", false);
        }

        // SpriteFlip 
        if (facingRight)
        {
            p_SpriteRenderer.flipX = false;
        }
        else
        {
            p_SpriteRenderer.flipX = true;
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currWait <= 0)
            {
                GameObject shot = Instantiate(shotPrefab, emitter.transform.position, Quaternion.identity);
                SnakeBullet sb = shot.gameObject.GetComponent<SnakeBullet>();
                if (facingRight)
                {
                    sb.ShootRight();
                }
                else
                {
                    sb.ShootLeft();
                }
                currWait = fireWait;
            }
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FacingRight()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            facingRight = true;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            facingRight = false;
        }
    }
}
