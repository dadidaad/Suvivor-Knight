using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    List<WeaponScriptableObject> listWeaponPrefabs;
    [SerializeField]
    List<PlayerScriptableObject> listCharacterPrefabs;
    [SerializeField]
    GameObject buttonPrefab;

    [SerializeField]
    Transform buttonPrefabParent;
    [SerializeField]
    ScriptableObjectType typeObject;
    private void Awake()
    {
        LoadButtons();   
    }

    private void LoadButtons()
    {
        if(typeObject == ScriptableObjectType.WeaponScriptableObject)
        {

            for (int i = 0; i < listWeaponPrefabs.Count; i++)
            {
                GameObject buttonObj = Instantiate(buttonPrefab, buttonPrefabParent) as GameObject;
                buttonObj.GetComponent<ButtonItem>().chooseView = gameObject;
                buttonObj.GetComponent<ButtonItem>().spriteObject = (listWeaponPrefabs[i]).Prefab;
                buttonObj.GetComponent<ButtonItem>().selectedObject = listWeaponPrefabs[i];
                buttonObj.GetComponent<ButtonItem>().buttonListControl = this;
            }
        }
        if (typeObject == ScriptableObjectType.PlayerScriptableObject)
        {
            for (int i = 0; i < listCharacterPrefabs.Count; i++)
            {
                GameObject buttonObj = Instantiate(buttonPrefab, buttonPrefabParent) as GameObject;
                buttonObj.GetComponent<ButtonItem>().chooseView = gameObject;
                buttonObj.GetComponent<ButtonItem>().spriteObject = (listCharacterPrefabs[i]).Character;
                buttonObj.GetComponent<ButtonItem>().selectedObject = listCharacterPrefabs[i];
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
        if(selectedObject is PlayerScriptableObject)
        {
            PlayerManager playerManager = FindObjectOfType<PlayerManager>();
            playerManager.SetupPlayer(selectedObject as PlayerScriptableObject);
        }
    }
}

public enum ScriptableObjectType
{
    WeaponScriptableObject,
    PlayerScriptableObject
}
