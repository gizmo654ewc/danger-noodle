using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemy_1;

    private GameObject spawnPoint;
    public GameObject spawnPoint_0;
    public GameObject spawnPoint_1;
    public GameObject spawnPoint_2;
    public GameObject spawnPoint_3;
    public GameObject spawnPoint_4;

    private int randSpawn;
    public int currNum;
    public int currPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            randSpawn = Random.Range(0, 5);
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
                SpawnEnemy(enemy_1, spawnPoint, currPoint);
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
                SpawnEnemy(enemy_1, spawnPoint, currPoint);
            }
            else if (currNum == 4)
            {
                currPoint = 2;
                spawnPoint = spawnPoint_4;
                SpawnEnemy(enemy_1, spawnPoint, currPoint);
            }
        }
    }
    void SpawnEnemy(GameObject prefab, GameObject spawnPoint, int currPoint)
    {
        GameObject enemy = Instantiate(prefab, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, 0), Quaternion.identity);
        EnemyMovementScript eScript = enemy.GetComponent<EnemyMovementScript>();
        eScript.currentPoint = currPoint;
        if (currNum == 3)
        {
            eScript.s3 = true;
        }
    }
}
