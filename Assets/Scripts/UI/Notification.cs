using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    TextMeshProUGUI guiText;
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowMessage(string message, float delay)
    {
        guiText = GetComponent<TextMeshProUGUI>();
        guiText.text = message;
        gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
