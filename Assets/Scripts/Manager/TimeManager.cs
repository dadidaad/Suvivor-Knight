using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    

    public static void PasueGame()
    {
        Time.timeScale = 0f;
    }

    public static void StartGame()
    {
        Time.timeScale = 1f;
    }

}
