﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    public GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }

    //Base stats for the weapon
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }

    [SerializeField]
    int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }

    [SerializeField]
    TypeWeapon type;

    public TypeWeapon Type { get => type; private set => type = value; }


    [SerializeField]
    public GameObject projectileWeapon;
    public GameObject ProjectileWeapon { get => type == TypeWeapon.Projectile? projectileWeapon : null; private set => projectileWeapon = value; }
    public enum TypeWeapon
    {
        Melee,
        Projectile
    }
}
