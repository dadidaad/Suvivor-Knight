using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    

    public static IEnumerator PasueGame(float timeInterval)
    {
        yield return new WaitForSeconds(timeInterval);
        Time.timeScale = 0f;
    }

}
