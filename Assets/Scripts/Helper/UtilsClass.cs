using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilsClass : MonoBehaviour
{
    public static float RandomNumber(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public static float GetAngleFromVector(Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg + 90;
    }
}
