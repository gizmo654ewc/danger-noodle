using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    //waypoints
    private GameObject wayPoint1;
    private GameObject[] wayPoint2s;
    private GameObject wayPoint2;
    private GameObject wayPoint3;
    private GameObject wayPoint3_S;
    private GameObject wayPoint4;

    Rigidbody2D rb;

    public float speed;
    public int currentPoint = 0;
    private bool climbing = false;
    private int chooser;
    private bool hit = false;
    private bool s3 = false;

    //hit stuff
    private GameObject currentPlat;
    [SerializeField] private BoxCollider2D enemyCollider;


    // Start is called before the first frame update
    void Start()
    {
        wayPoint1 = GameObject.FindWithTag("Waypoint_1");

        wayPoint2s = GameObject.FindGameObjectsWithTag("Waypoint_2");
        chooser = Random.Range(0, wayPoint2s.Length);
        wayPoint2 = wayPoint2s[chooser];

        wayPoint3 = GameObject.FindWithTag("Waypoint_3");
        wayPoint3_S = GameObject.FindWithTag("Waypoint_3_S");

        wayPoint4 = GameObject.FindWithTag("Waypoint_4");

        rb = GetComponent<Rigidbody2D>();
        if (currentPoint == 0)
        {
            rb.AddForce(new Vector2(0, 1400));
        }
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (!hit)
        {
            if (currentPoint <= 0)
            {
                s3 = false;
                ClimbTo(wayPoint1);
            }
            if (currentPoint == 1)
            {
                s3 = false;
                ClimbTo(wayPoint2);
            }
            if (currentPoint == 2)
            {
                if (s3)
                {
                    ClimbTo(wayPoint3_S);
                }
                else
                {
                    ClimbTo(wayPoint3);
                }
            }
            if (currentPoint == 3)
            {
                s3 = false;
                ClimbTo(wayPoint4);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shot")
        {
            Destroy(collision.gameObject);
            hit = true;
            rb.velocity = Vector2.zero;
            StartCoroutine(DisableCollision());
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

            if (wayPoint.transform.position.x - .1f >= transform.position.x ^ transform.position.x <= wayPoint.transform.position.x + .1f)
            {
                transform.position = new Vector2(wayPoint.transform.position.x, transform.position.y);
                rb.velocity = Vector2.zero;
                climbing = true;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (wayPoint.transform.position.y < transform.position.y)
            {
                transform.position = new Vector2(transform.position.x, wayPoint.transform.position.y);
                rb.velocity = Vector2.zero;
                climbing = false;
                currentPoint++;
            }
        }
    }


    //hit stuff
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") ^ collision.gameObject.CompareTag("Ground"))
        {
            currentPlat = collision.gameObject;
        }
        
        if (collision.gameObject.CompareTag("KillPlane"))
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") ^ collision.gameObject.CompareTag("Ground"))
        {
            currentPlat = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        if (currentPlat != null)
        {
            if (climbing)
            {
                climbing = false;
                BoxCollider2D platformCollider = currentPlat.GetComponent<BoxCollider2D>();
                Physics2D.IgnoreCollision(enemyCollider, platformCollider);
                yield return new WaitForSeconds(.8f);
                Physics2D.IgnoreCollision(enemyCollider, platformCollider, false);
            }
            else
            {
                climbing = false;
                BoxCollider2D platformCollider = currentPlat.GetComponent<BoxCollider2D>();
                Physics2D.IgnoreCollision(enemyCollider, platformCollider);
                yield return new WaitForSeconds(.5f);
                Physics2D.IgnoreCollision(enemyCollider, platformCollider, false);
            }
        }
        climbing = false;
        yield return new WaitForSeconds(.5f);
        GameObject[] waypoints = { wayPoint1, wayPoint2, wayPoint3, wayPoint4 };
        if (FindClosestWP(waypoints).CompareTag("Waypoint_1"))
        {
            Debug.Log("closest is 0");
            currentPoint = 0;
        }
        else if (FindClosestWP(waypoints).CompareTag("Waypoint_2"))
        {
            Debug.Log("closest is 1");
            currentPoint = 1;
            chooser = Random.Range(0, wayPoint2s.Length);
            wayPoint2 = wayPoint2s[chooser];
        }
        else if (FindClosestWP(waypoints).CompareTag("Waypoint_3"))
        {
            Debug.Log("closest is 2");
            currentPoint = 2;
            if (transform.position.x < wayPoint3_S.transform.position.x + 3 && transform.position.x > wayPoint3_S.transform.position.x - 1.5f)
            {
                Debug.Log("s3 is now true");
                s3 = true;
            }
            else { s3 = false; }
        }
        else if (FindClosestWP(waypoints).CompareTag("Waypoint_4"))
        {
            Debug.Log("closest is 3");
            currentPoint = 3;
        }
        hit = false;
        climbing = false;
        Debug.Log(currentPoint);
        Debug.Log(climbing);
    }

    private GameObject FindClosestWP(GameObject[] waypoints)
    {
        GameObject closestWP = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in waypoints)
        {
            float dist = Vector2.Distance(new Vector2(0, t.transform.position.y - 5), new Vector2(0, currentPos.y));
            if (t == wayPoint4)
            {
                dist = Vector2.Distance(new Vector2(0, t.transform.position.y - 8), new Vector2(0, currentPos.y));
            }
            if (dist < minDist)
            {
                closestWP = t;
                minDist = dist;
            }
        }
        return closestWP;
    }
}
