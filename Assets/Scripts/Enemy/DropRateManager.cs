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
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public List<Drops> drops;

    void OnDestroy()
    {
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
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
