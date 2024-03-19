using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemy_1;

    GameObject[] spawners;
    private GameObject spawnPoint;

    private int randSpawn;

    // Start is called before the first frame update
    void Start()
    {
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            randSpawn = Random.Range(0, spawners.Length);
            spawnPoint = spawners[randSpawn];
            Debug.Log(randSpawn);
            GameObject enemy = Instantiate(enemy_1, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, 0), Quaternion.identity);
            EnemyMovementScript eScript = enemy.GetComponent<EnemyMovementScript>();
            if (randSpawn > 2)
            {
                eScript.currentPoint = 0;
            }
        }
    }
}
