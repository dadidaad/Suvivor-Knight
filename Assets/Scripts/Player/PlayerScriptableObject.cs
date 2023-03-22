using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName ="ScriptableObjects/Character")]
public class PlayerScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject character;
    public GameObject Character { get => character; private set => character = value; }

    //[SerializeField]
    //GameObject startingWeapon;
    //public GameObject StartingWeapon { get => startingWeapon; private set => startingWeapon = value; }

    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    float recovery;
    public float Recovery { get => recovery; private set => recovery = value; }

    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }  



}
