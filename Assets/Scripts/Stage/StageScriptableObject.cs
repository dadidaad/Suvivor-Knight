using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageScriptableObject", menuName = "ScriptableObjects/Stage")]
public class StageScriptableObject : ScriptableObject
{
    public List<StageEvent> stageEvents;
}

[Serializable]
public class StageEvent
{
    [SerializeField]
    float time;
    public float Time { get => time; private set => time = value; }
    [SerializeField]
    List<EnemyGroup> wave;
    public List<EnemyGroup> Wave { get => wave; private set => wave = value; }
}

[Serializable]
public class EnemyGroup
{
    public int enemyCount; //number of enemy spawn this way
    public GameObject enemyPrefab;
}