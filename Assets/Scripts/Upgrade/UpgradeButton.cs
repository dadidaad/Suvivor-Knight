using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Text displayText;

    public void Set (UpgradeData upgradeData)
    {
        icon.sprite = upgradeData.icon;
        displayText.text = upgradeData.Name;
    }

/*    public void Update(int pressedButtonID)
    {
        UpgradeData upgradeData = 
    }*/
}
