using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHelper : MonoBehaviour
{
    public static float RandomNumber(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}
