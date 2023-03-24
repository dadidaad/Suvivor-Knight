using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    StageScriptableObject stageData;

    StageTime stageTime;
    [SerializeField]
    EnemyManager enemyManager;
    int eventIndexer;

    float furyStateDuration = 30f;
    [SerializeField]
    List<float> furyStartTime;
    bool isFuryStage = false;
    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(eventIndexer >= stageData.stageEvents.Count)
        {
            return;
        }
        if(stageTime.time > stageData.stageEvents[eventIndexer].Time)
        {
            for (int i = 0; i < stageData.stageEvents[eventIndexer].Wave.Count; i++)
            {
                EnemyGroup enemyGroup = stageData.stageEvents[eventIndexer].Wave[i];
                StartCoroutine(SpawnEnemy(enemyGroup));
            }
            eventIndexer++;
        }
        CheckFuryStage();
        if (isFuryStage)
        {
            Notification notification = GameManager.Instance.notificationPanel.GetComponent<Notification>();
            StartCoroutine(notification.ShowMessage("FURY STATE!!", 1f));
            isFuryStage = false;
        }
    }

    IEnumerator SpawnEnemy(EnemyGroup enemyGroup)
    {
        for(int i=1; i < enemyGroup.enemyCount; i++)
        {
            enemyManager.SpawnEnemy(enemyGroup.enemyPrefab);
            yield return new WaitForSeconds(1f); 
        }
    }

    void CheckFuryStage()
    {
        foreach (float furyStage in furyStartTime)
        {
            if (Mathf.CeilToInt(stageTime.time) == furyStage)
            {
                isFuryStage = true;
                break;
            }
        }
    }
}
