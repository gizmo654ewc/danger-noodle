using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //spawn system
    private float waitTimeBeforeSpawn = 3;
    private float currWaitTime;

    //score
    public int scoreCount;
    private GameObject scoreObj;
    ScoreHolder scoreScript;

    [SerializeField] private GameObject enemy_1;
    [SerializeField] private GameObject enemy_2;
    [SerializeField] private GameObject enemy_3;
    [SerializeField] private GameObject powerup;

    private GameObject spawnPoint;
    public GameObject spawnPoint_0;
    public GameObject spawnPoint_1;
    public GameObject spawnPoint_2;
    public GameObject spawnPoint_3;
    public GameObject spawnPoint_4;
    public GameObject spawnPoint_5;
    public GameObject spawnPoint_powerup;

    private int randSpawn;
    public int currNum;
    public int currPoint;
    public bool done = false;
    public float waitUntilPowerup;
    private float currWait;


    // Start is called before the first frame update
    void Start()
    {
        currWait = waitUntilPowerup;
        //spawntime
        currWaitTime = waitTimeBeforeSpawn;
        //score
        scoreObj = GameObject.FindWithTag("Score");
        scoreScript = scoreObj.GetComponent<ScoreHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currWait > 0)
        {
            currWait -= Time.deltaTime;
        }
        else
        {
            if (!done)
            {
                Instantiate(powerup, new Vector2(spawnPoint_powerup.transform.position.x, spawnPoint_powerup.transform.position.y), Quaternion.identity);
                done = true;
                currWait = waitUntilPowerup;
                Debug.Log("spawned");
            }
        }

        scoreCount = scoreScript.score;
        if (scoreCount < 100)
        {
            waitTimeBeforeSpawn = 2;
        }
        else if (scoreCount < 350)
        {
            waitTimeBeforeSpawn = 2.2f;
        }
        else if (scoreCount < 1000)
        {
            waitTimeBeforeSpawn = 2;
        }
        else if (scoreCount < 1500)
        {
            waitTimeBeforeSpawn = 1.5f;
        }
        else if (scoreCount < 2000)
        {
            waitTimeBeforeSpawn = 1.2f;
        }
        else if (scoreCount < 3000)
        {
            waitTimeBeforeSpawn = 1f;
        }
        else if (scoreCount < 4000)
        {
            waitTimeBeforeSpawn = 0.9f;
        }
        else if (scoreCount < 5000)
        {
            waitTimeBeforeSpawn = 0.2f;
        }

        if (currWaitTime > 0)
        {
            currWaitTime = currWaitTime - Time.deltaTime;
        }
        else if (currWaitTime <= 0.1)
        {
            if (scoreCount < 200)
            {
                randSpawn = Random.Range(0, 1);
            }
            else if (scoreCount < 500)
            {
                randSpawn = Random.Range(0, 3);
            }
            else if (scoreCount < 1000)
            {
                randSpawn = Random.Range(0, 5);
            }
            else if (scoreCount < 1500)
            {
                randSpawn = Random.Range(0, 6);
            }
            else if (scoreCount < 2000)
            {
                randSpawn = Random.Range(0, 6);
            }
            else if (scoreCount >= 2000)
            {
                randSpawn = Random.Range(0, 6);
            }
            currNum = randSpawn;
            Debug.Log(currNum);
            if (currNum == 0)
            {
                currPoint = 0;
                spawnPoint = spawnPoint_0;
                SpawnEnemy(enemy_1, spawnPoint, currPoint);
            }
            else if (currNum == 1)
            {
                currPoint = 1;
                spawnPoint = spawnPoint_1;
                SpawnEnemy(enemy_2, spawnPoint, currPoint);
            }
            else if (currNum == 2)
            {
                currPoint = 1;
                spawnPoint = spawnPoint_2;
                SpawnEnemy(enemy_1, spawnPoint, currPoint);
            }
            else if (currNum == 3)
            {
                currPoint = 2;
                spawnPoint = spawnPoint_3;
                SpawnEnemy(enemy_2, spawnPoint, currPoint);
            }
            else if (currNum == 4)
            {
                currPoint = 2;
                spawnPoint = spawnPoint_4;
                SpawnEnemy(enemy_1, spawnPoint, currPoint);
            }
            else if (currNum == 5)
            {
                currPoint = 2;
                spawnPoint = spawnPoint_5;
                SpawnEnemy(enemy_3, spawnPoint, currPoint);
            }
            currWaitTime = waitTimeBeforeSpawn;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            randSpawn = Random.Range(0, 6);
            currNum = randSpawn;
            Debug.Log(currNum);
            if (currNum == 0)
            {
                currPoint = 0;
                spawnPoint = spawnPoint_0;
                SpawnEnemy(enemy_1, spawnPoint, currPoint);
            }
            else if (currNum == 1)
            {
                currPoint = 1;
                spawnPoint = spawnPoint_1;
                SpawnEnemy(enemy_2, spawnPoint, currPoint);
            }
            else if (currNum == 2)
            {
                currPoint = 1;
                spawnPoint = spawnPoint_2;
                SpawnEnemy(enemy_1, spawnPoint, currPoint);
            }
            else if (currNum == 3)
            {
                currPoint = 2;
                spawnPoint = spawnPoint_3;
                SpawnEnemy(enemy_2, spawnPoint, currPoint);
            }
            else if (currNum == 4)
            {
                currPoint = 2;
                spawnPoint = spawnPoint_4;
                SpawnEnemy(enemy_1, spawnPoint, currPoint);
            }
            else if (currNum == 5)
            {
                currPoint = 2;
                spawnPoint = spawnPoint_5;
                SpawnEnemy(enemy_3, spawnPoint, currPoint);
            }
        }
    }
    void SpawnEnemy(GameObject prefab, GameObject spawnPoint, int currPoint)
    {
        if (currNum <= 4) 
        {
            GameObject enemy = Instantiate(prefab, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, 0), Quaternion.identity);
            EnemyMovementScript eScript = enemy.GetComponent<EnemyMovementScript>();
            eScript.currentPoint = currPoint;
            if (currNum == 3)
            {
                eScript.s3 = true;
            }
        }
        else if (currNum == 5)
        {
            GameObject roidenemy = Instantiate(prefab, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, 0), Quaternion.identity);
            RoidEnemyMovementScript reScript = roidenemy.GetComponent<RoidEnemyMovementScript>();
            reScript.currentPoint = currPoint;
        }
    }
}
