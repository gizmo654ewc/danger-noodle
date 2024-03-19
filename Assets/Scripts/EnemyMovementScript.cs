using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    //waypoints
    private GameObject wayPoint1;
    private GameObject[] wayPoint2s;
    private GameObject wayPoint2;

    Rigidbody2D rb;

    public float speed;
    public int currentPoint = 0;
    private bool climbing = false;
    private int chooser;


    // Start is called before the first frame update
    void Start()
    {
        wayPoint1 = GameObject.FindWithTag("Waypoint_1");
        wayPoint2s = GameObject.FindGameObjectsWithTag("Waypoint_2");
        chooser = Random.Range(0, wayPoint2s.Length);
        wayPoint2 = wayPoint2s[chooser];
        rb = GetComponent<Rigidbody2D>();
        if (currentPoint == 0)
        {
            rb.AddForce(new Vector2(0, 1400));
        }
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (currentPoint == 0)
        {
            ClimbTo(wayPoint1);
        }
        if (currentPoint == 1)
        {
            ClimbTo(wayPoint2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shot")
        {
            Destroy(collision.gameObject);

        }
    }

    private void ClimbTo(GameObject wayPoint)
    {
        rb = GetComponent<Rigidbody2D>();

        if (!climbing)
        {
            if (transform.position.x > wayPoint.transform.position.x)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }

            if (wayPoint.transform.position.x - .2f >= transform.position.x ^ transform.position.x <= wayPoint.transform.position.x + .2f)
            {
                transform.position = new Vector2(wayPoint.transform.position.x, transform.position.y);
                rb.velocity = new Vector2(0, 0);
                climbing = true;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (wayPoint.transform.position.y < transform.position.y)
            {
                transform.position = new Vector2(transform.position.x, wayPoint.transform.position.y);
                rb.velocity = new Vector2(0, 0);
                climbing = false;
                currentPoint++;
            }
        }
    }
    
}
