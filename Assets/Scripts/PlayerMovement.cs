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
    // Animator and Sprite Flipper
    Animator Snake;
    SpriteRenderer pSprite;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private GameObject emitter;

    [SerializeField] private AudioClip jumpSoundClip;
    [SerializeField] private AudioClip shootSoundClip;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Animator & Sprite Flip Components
        Snake = GetComponent<Animator>();
        pSprite = GetComponent<SpriteRenderer>();
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
        // Made a Float to initiate movement animation
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        Snake.SetFloat("moveSpeed", Mathf.Abs(horizontalInput));

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
            AudioSource.PlayClipAtPoint(jumpSoundClip, transform.position, 0.14f);
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            // Jump trigger
            Snake.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AudioSource.PlayClipAtPoint(shootSoundClip, transform.position, 0.085f);
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

        // Grounded param
        if (IsGrounded())
        {
            Snake.SetBool("isGrounded", true);
        }
        else
        {
            Snake.SetBool("isGrounded", false);
        }
        
        // SpriteFlip 
        if (facingRight)
        {
            pSprite.flipX = false;
        }
        else
        {
            pSprite.flipX = true;
        }
    }

    private bool IsGrounded()
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
