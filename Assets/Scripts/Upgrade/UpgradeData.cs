using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    WeaponUpgrade,
    WeaponUnlock,
    PlayerUpgrade
}

public enum Star
{
    MoveSpeed,
    Health,
    Recover,
    Cooldown,
    Damage,
    Speed
}

[CreateAssetMenu(fileName = "UpgradeDataScriptableObject", menuName = "ScriptableObjects/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public UpgradeType upgradeType;
    public Star star;
    public float value;
    public string Name;
    public Sprite icon;
}
