using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static void DoFloatingText(Vector3 position, string text, Color c)
    {
        EffectsManager effectsManager = FindObjectOfType<EffectsManager>();
        GameObject floatingText = Instantiate(effectsManager.damagePrefab, position, Quaternion.identity);
        floatingText.GetComponent<TMP_Text>().color = c;
        floatingText.GetComponent<TextPopUp>().displayText = text;
    }
}

