using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    //waypoints
    private GameObject wayPoint1;

    Rigidbody2D rb;

    public float speed;
    public int currentPoint = 0;
    private bool climbing = false;


    // Start is called before the first frame update
    void Start()
    {
        wayPoint1 = GameObject.FindWithTag("Waypoint_1");
        rb = GetComponent<Rigidbody2D>();
        if (currentPoint == 0)
        {
            rb.AddForce(new Vector2(0, 1000));
        }
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (currentPoint == 0)
        {
            if (!climbing)
            {
                if (transform.position.x > wayPoint1.transform.position.x)
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }

                if (wayPoint1.transform.position.x-.2f >= transform.position.x ^ transform.position.x <= wayPoint1.transform.position.x+.2f)
                {
                    transform.position = new Vector2(wayPoint1.transform.position.x, transform.position.y);
                    rb.velocity = new Vector2(0, 0);
                    climbing = true;
                }
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, speed);
                if (wayPoint1.transform.position.y < transform.position.y)
                {
                    transform.position = new Vector2(transform.position.x, wayPoint1.transform.position.y);
                    rb.velocity = new Vector2(0, 0);
                    climbing = false;
                    currentPoint = 1;
                }
            }
            
        }
        if (currentPoint == 1)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shot")
        {
            Destroy(collision.gameObject);
        }
    }

    
}
