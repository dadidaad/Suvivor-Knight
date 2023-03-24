using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField]
    List<GameObject> spawnPoints;

    [SerializeField]
    GameObject toSpawn;

    [SerializeField]
    [Range(0f, 1f)]
    float probability;

    
    Transform player;
    float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 1f)
        {
            Spawn();
            time = 0f;
        }
    }

    public void Spawn()
    {
        if(Random.value <= probability)
        {
            GameObject chestPrefab = Instantiate(toSpawn, player.position + spawnPoints[(int)UtilsClass.RandomNumber(0f, 2f)].transform.position, Quaternion.identity);
            GameObject terrianCurrent = FindObjectOfType<TilemapController>().currentChunk;
            if(terrianCurrent != null)
            {
                chestPrefab.transform.SetParent(terrianCurrent.transform);
            }

        }
    }
}
