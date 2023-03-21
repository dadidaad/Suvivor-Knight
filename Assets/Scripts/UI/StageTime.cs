using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTime : MonoBehaviour
{
    public float time;
    TimeUI timeUI;

    private void Awake()
    {
        timeUI = FindObjectOfType<TimeUI>();
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeUI.UpdateTime(time);
    }
}
