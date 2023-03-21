using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsOnDestroy : MonoBehaviour
{
    [SerializeField]
    List<GameObject> drops;

    [SerializeField]
    [Range(0f, 1f)]
    float chance = 1f;

    bool isQuiting = false;

    private void OnApplicationQuit()
    {
        isQuiting = true;
    }
    private void OnDestroy()
    {
        if (isQuiting) { return; }
        if (Random.value < chance)
        {
            Transform item = Instantiate(drops[(int)UtilsClass.RandomNumber(0f, 1f)]).transform;
            item.position = transform.position;
            GameObject terrianCurrent = FindObjectOfType<TilemapController>().currentChunk;
            item.SetParent(terrianCurrent.transform);
        }
    }
}
