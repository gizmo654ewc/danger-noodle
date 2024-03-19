using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    //waypoints
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject wayPoint1_1;
    [SerializeField] private GameObject wayPoint1_2;
    //[SerializeField] private GameObject wayPoint2_1;
    //[SerializeField] private GameObject wayPoint2_2;

    Rigidbody2D rb;

    public float speed;
    public int currentPoint = 0;
    private bool climbing = false;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnPoint.transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, 1000));
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPoint == 0)
        {
            if (!climbing)
            {
                if (transform.position.x > wayPoint1_1.transform.position.x)
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }

                if (wayPoint1_1.transform.position.x-.2f >= transform.position.x ^ transform.position.x <= wayPoint1_1.transform.position.x+.2f)
                {
                    transform.position = new Vector2(wayPoint1_1.transform.position.x, transform.position.y);
                    rb.velocity = new Vector2(0, 0);
                    climbing = true;
                }
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, speed);
                if (wayPoint1_2.transform.position.y < transform.position.y)
                {
                    transform.position = new Vector2(transform.position.x, wayPoint1_2.transform.position.y);
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
}
