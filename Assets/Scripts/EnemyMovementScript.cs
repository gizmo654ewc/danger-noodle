using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    //waypoints
    private GameObject wayPoint1;

    private GameObject[] wayPoint2s;
    private GameObject wayPoint2;

    private GameObject wayPoint3;
    private GameObject wayPoint3_S;

    private GameObject ANNOYINGFLOOR;

    private GameObject wayPoint4;
    private GameObject wayPoint4s1;
    private GameObject wayPoint4s2;
    private GameObject currWayPoint4;
    private bool altRoute = false;


    private GameObject wayPoint5;
    private GameObject wayPoint6;

    Rigidbody2D rb;
    Animator bRat_Anim;
    SpriteRenderer bR_SpriteRenderer;

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

        ANNOYINGFLOOR = GameObject.FindWithTag("Waypoint_3_FLOOR");

        wayPoint4 = GameObject.FindWithTag("Waypoint_4");
        wayPoint4s1 = GameObject.FindWithTag("Waypoint_4s1");
        wayPoint4s2 = GameObject.FindWithTag("Waypoint_4s2");
        chooser = Random.Range(0, 2);
        if (chooser == 0)
        {
            currWayPoint4 = wayPoint4;
        }
        else if (chooser == 1)
        {
            currWayPoint4 = wayPoint4s1;
        }



        wayPoint5 = GameObject.FindWithTag("Waypoint_5");

        wayPoint6 = GameObject.FindWithTag("Waypoint_6");

        bRat_Anim = GetComponent<Animator>();
        bR_SpriteRenderer = GetComponent<SpriteRenderer>();
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
                if (currWayPoint4 == wayPoint4s1)
                {
                    altRoute = true;
                }
                ClimbTo(currWayPoint4);
            }
            if (currentPoint == 4)
            {
                altRoute = false;
                ClimbTo(wayPoint5);
            }
            if (currentPoint == 5)
            {
                ClimbTo(wayPoint6);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shot")
        {
            Destroy(collision.gameObject);
            if (!hit)
            {
                rb.velocity = Vector2.zero;
                StartCoroutine(DisableCollision());
                hit = true;
                bRat_Anim.SetTrigger("Hurt");
            }
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
                bRat_Anim.SetBool("isClimbing", true);
            }
            else
            {
                bRat_Anim.SetBool("isClimbing", false);
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
                if (altRoute)
                {
                    altRoute = false;
                    currWayPoint4 = wayPoint4s2;
                }
                else
                {
                    currentPoint++;
                }
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
                gameObject.layer = 9;
                Debug.Log("begin");
                yield return new WaitForSeconds(.5f);
            }
            else
            {
                climbing = false;
                gameObject.layer = 9;
                Debug.Log("begin");
                yield return new WaitForSeconds(.35f);
            }
        }
        gameObject.layer = 7;
        Debug.Log("over");
        climbing = false;
        yield return new WaitForSeconds(.5f);
        GameObject[] waypoints = { wayPoint1, wayPoint2, wayPoint3, wayPoint4, wayPoint4s2, wayPoint5, wayPoint6 };
        if (FindClosestWP(waypoints).CompareTag("Waypoint_1"))
        {
            currentPoint = 0;
        }
        else if (FindClosestWP(waypoints).CompareTag("Waypoint_2"))
        {
            currentPoint = 1;
            chooser = Random.Range(0, wayPoint2s.Length);
            wayPoint2 = wayPoint2s[chooser];
        }
        else if (FindClosestWP(waypoints).CompareTag("Waypoint_3"))
        {
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
            chooser = Random.Range(0, 2);
            if (chooser == 0)
            {
                currWayPoint4 = wayPoint4;
            }
            else if (chooser == 1)
            {
                currWayPoint4 = wayPoint4s1;
            }
            currentPoint = 3;
        }
        else if (FindClosestWP(waypoints).CompareTag("Waypoint_4s2"))
        {
            currentPoint = 3;
        }
        else if (FindClosestWP(waypoints).CompareTag("Waypoint_5"))
        {
            currentPoint = 4;
        }
        else if (FindClosestWP(waypoints).CompareTag("Waypoint_6"))
        {
            currentPoint = 5;
        }
        hit = false;
        climbing = false;
    }

    private GameObject FindClosestWP(GameObject[] waypoints)
    {
        ANNOYINGFLOOR = GameObject.FindWithTag("Waypoint_3_FLOOR");
        GameObject closestWP = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in waypoints)
        {
            float dist = Vector2.Distance(new Vector2(0, t.transform.position.y - 5), new Vector2(0, currentPos.y));
            if (t == wayPoint4)
            {
                dist = Vector2.Distance(new Vector2(0, ANNOYINGFLOOR.transform.position.y), new Vector2(0, currentPos.y));
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
