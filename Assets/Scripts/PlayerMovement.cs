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
    private bool powerup = false;
    public float powerupTime;
    private float currPT = 0;

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

        if (currPT > 0)
        {
            currPT -= Time.deltaTime;
        }
        else if (currPT < 0.1)
        {
            powerup = false;
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
        if (Input.GetKey(KeyCode.Z))
        {
            if (powerup)
            {
                if (currWait > 0)
                {
                    currWait -= Time.deltaTime;
                }
                else
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
                    currWait = 0.05f;
                }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyShot")
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump/2));
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Powerup")
        {
            powerup = true;
            currPT += powerupTime;
            Destroy(collision.gameObject);
        }
    }
}
