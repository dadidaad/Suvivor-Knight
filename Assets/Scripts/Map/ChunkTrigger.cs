using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    TilemapController tilemapController;
    public GameObject targetMap;
    // Start is called before the first frame update
    void Start()
    {
        tilemapController = FindObjectOfType<TilemapController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tilemapController.currentChunk = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(tilemapController.currentChunk == targetMap)
            {
                tilemapController.currentChunk = null;
            }
        }   
    }
}
