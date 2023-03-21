using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    List<ScriptableObject> listPrefabs;

    [SerializeField]
    GameObject buttonPrefab;

    [SerializeField]
    Transform buttonPrefabParent;
    [SerializeField]
    ScriptableObjectType typeObject;
    private void Start()
    {
        LoadButtons();   
    }

    private void LoadButtons()
    {
        if(typeObject == ScriptableObjectType.WeaponScriptableObject)
        {
            for (int i = 0; i < listPrefabs.Count; i++)
            {
                if(listPrefabs[i] is PlayerScriptableObject)
                {
                    listPrefabs.RemoveAt(i);
                }
            }
            for (int i = 0; i < listPrefabs.Count; i++)
            {
                GameObject buttonObj = Instantiate(buttonPrefab, buttonPrefabParent) as GameObject;
                buttonObj.GetComponent<ButtonItem>().spriteObject = (listPrefabs[i] as WeaponScriptableObject).Prefab;
                buttonObj.GetComponent<ButtonItem>().selectedObject = listPrefabs[i];
                buttonObj.GetComponent<ButtonItem>().buttonListControl = this;
            }
        }
        if (typeObject == ScriptableObjectType.PlayerScriptableObject)
        {
            for (int i = 0; i < listPrefabs.Count; i++)
            {
                if (listPrefabs[i] is WeaponScriptableObject)
                {
                    listPrefabs.RemoveAt(i);
                }
            }
            for (int i = 0; i < listPrefabs.Count; i++)
            {
                GameObject buttonObj = Instantiate(buttonPrefab, buttonPrefabParent) as GameObject;
                buttonObj.GetComponent<ButtonItem>().spriteObject = (listPrefabs[i] as PlayerScriptableObject).Character;
                buttonObj.GetComponent<ButtonItem>().selectedObject = listPrefabs[i];
                buttonObj.GetComponent<ButtonItem>().buttonListControl = this;
            }
        }
    }

    public void OnButtonClick(ScriptableObject selectedObject)
    { 
        if(selectedObject is WeaponScriptableObject)
        {
            WeaponManager weaponManager = FindObjectOfType<WeaponManager>();
            weaponManager.SetupWeapon(selectedObject as WeaponScriptableObject);
        }
    }
}

public enum ScriptableObjectType
{
    WeaponScriptableObject,
    PlayerScriptableObject
}
