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

    Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private GameObject emitter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FacingRight();
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);

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
        }

        if (Input.GetKeyDown(KeyCode.Z))
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
