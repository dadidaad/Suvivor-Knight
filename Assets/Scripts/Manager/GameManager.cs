using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> listSelectorView;
    [HideInInspector]
    public int state = 0;

    public static GameManager Instance{ get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        Time.timeScale = 0f;
        LoadViewSelector();
    }

    private void Update()
    {
        LoadViewSelector();
    }
    public static void DoFloatingText(Vector3 position, string text, Color c)
    {
        EffectsManager effectsManager = FindObjectOfType<EffectsManager>();
        GameObject floatingText = Instantiate(effectsManager.damagePrefab, position, Quaternion.identity);
        floatingText.GetComponent<TMP_Text>().color = c;
        floatingText.GetComponent<TextPopUp>().displayText = text;
    }


    void LoadViewSelector()
    {
        if(state < listSelectorView.Count)
        {
            listSelectorView[state].SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}

