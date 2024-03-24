using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RoidEnemyMovementScript : MonoBehaviour
{
    //shot system
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private float waitTimeBeforeShot;
    private float currWaitTime;

    //score
    public int scoreCount;
    private GameObject scoreObj;
    ScoreHolder scoreScript;

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

    private GameObject[] wayPoint6s;
    private GameObject wayPoint6;

    private GameObject wayPointFinal;

    Rigidbody2D rb;
    Animator roidRat_Anim;

    public float speed;
    public int currentPoint;
    private bool climbing = false;
    private int chooser;
    private bool stop;
    public bool s3 = false;

    //stop stuff
    private GameObject currentPlat;
    [SerializeField] private BoxCollider2D enemyCollider;

    //enemy audio
    [SerializeField] private AudioClip scoreSoundClip;


    // Start is called before the first frame update
    void Start()
    {
        currWaitTime = waitTimeBeforeShot;
        //score 
        scoreObj = GameObject.FindWithTag("Score");
        scoreScript = scoreObj.GetComponent<ScoreHolder>();

        //waypoints
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

        wayPoint6s = GameObject.FindGameObjectsWithTag("Waypoint_6");
        chooser = Random.Range(0, wayPoint6s.Length);
        wayPoint6 = wayPoint6s[chooser];

        wayPointFinal = GameObject.FindWithTag("Waypoint_Final");

        roidRat_Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stop = true;
        StartCoroutine(Punch());

    }


    // Update is called once per frame
    void Update()
    {
        //shots
        if (currWaitTime > 0)
        {
            currWaitTime = currWaitTime - Time.deltaTime;
        }
        else if (currWaitTime <= 0.1)
        {
            if ((Random.Range(0, 10)) == 1)
            {
                Instantiate(shotPrefab, transform.position, Quaternion.identity);
                roidRat_Anim.SetTrigger("Punch");
            }
            currWaitTime = 5;
        }

        if (!stop)
        {
            if (currentPoint == 0)
            {
                ClimbTo(wayPoint1);
            }
            else if (currentPoint == 1)
            {
                s3 = false;
                ClimbTo(wayPoint2);
            }
            else if (currentPoint == 2)
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
            else if (currentPoint == 3)
            {
                if (currWayPoint4 == wayPoint4s1)
                {
                    altRoute = true;
                }
                ClimbTo(currWayPoint4);
            }
            else if (currentPoint == 4)
            {
                altRoute = false;
                ClimbTo(wayPoint5);
            }
            else if (currentPoint == 5)
            {
                ClimbTo(wayPoint6);
            }
            else if (currentPoint == 6)
            {
                ClimbTo(wayPointFinal);
            }
            else if (currentPoint >= 7)
            {

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shot")
        {
            Destroy(collision.gameObject);
            if (!stop)
            {
                rb.velocity = Vector2.zero;
                StartCoroutine(DisableCollision());
                stop = true;
                roidRat_Anim.SetTrigger("bigHurt");
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
                roidRat_Anim.SetBool("isClimbing", true);
            }
            else
            {
                roidRat_Anim.SetBool("isClimbing", false);
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


    //stop stuff
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") ^ collision.gameObject.CompareTag("Ground"))
        {
            currentPlat = collision.gameObject;
        }

        if (collision.gameObject.CompareTag("KillPlane"))
        {
            AudioSource.PlayClipAtPoint(scoreSoundClip, transform.position, 1f);
            Debug.Log("did it");
            scoreScript.UpdateScore(scoreCount);
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
                yield return new WaitForSeconds(.5f);
            }
            else
            {
                climbing = false;
                gameObject.layer = 9;
                yield return new WaitForSeconds(.35f);
            }
        }
        gameObject.layer = 7;
        climbing = false;
        yield return new WaitForSeconds(.5f);
        GameObject[] waypoints = { wayPoint1, wayPoint2, wayPoint3, wayPoint4, wayPoint4s2, wayPoint5, wayPoint6, wayPointFinal };
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
            chooser = Random.Range(0, wayPoint6s.Length);
            wayPoint6 = wayPoint6s[chooser];
        }
        else if (FindClosestWP(waypoints).CompareTag("Waypoint_Final"))
        {
            currentPoint = 6;
        }
        stop = false;
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

    private IEnumerator Punch()
    {
        roidRat_Anim.SetBool("isBreaking", true);
        yield return new WaitForSeconds(1f);
        stop = false;
        roidRat_Anim.SetBool("isBreaking", false);
    }
}
