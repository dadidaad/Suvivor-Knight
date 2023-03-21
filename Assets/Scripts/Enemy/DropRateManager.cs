using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    
    [System.Serializable] 
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
        public int experienceGrant;
    }
    public List<Drops> drops;
    bool isQuiting = false;

    private void OnApplicationQuit()
    {
        isQuiting = true;
    }
    void OnDestroy()
    {
        if(isQuiting) { return; }
        float randomNumber = UtilsClass.RandomNumber(0f, 100f);
        List<Drops> possibleDrops = new List<Drops>();
        foreach(Drops drop in drops)
        {
            if(randomNumber <= drop.dropRate)
            {
                possibleDrops.Add(drop);
            }
        }
        if(possibleDrops.Count > 0)
        {
            Drops drops = possibleDrops[Mathf.FloorToInt(UtilsClass.RandomNumber(0f, possibleDrops.Count))];
            GameObject star = Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
            GameObject terrianCurrent = FindObjectOfType<TilemapController>().currentChunk;
            star.transform.SetParent(terrianCurrent.transform);
        }
    }
}
