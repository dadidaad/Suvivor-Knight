using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField]
    Vector2 spawnArea;

    [SerializeField]
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public  void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 spawnPosition = GenerateRandomPosition();

        spawnPosition += player.transform.position;
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.position = spawnPosition;
        newEnemy.transform.parent = transform;
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new Vector3();
        float randomRange = Random.value > 0.5 ? -1f : 1f;
        if(Random.value > 0.5f)
        {
            position.x = UtilsClass.RandomNumber(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * randomRange;
        }
        else
        {
            position.y = UtilsClass.RandomNumber(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * randomRange;
        }
        position.z = 0f;
        return position;
    }

}
