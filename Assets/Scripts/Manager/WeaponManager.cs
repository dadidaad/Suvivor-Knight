using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [SerializeField]
    Transform weaponParent;

    [HideInInspector]
    public WeaponScriptableObject weaponScriptableObject;
    [SerializeField]
    GameObject chooseWeaponView;
    bool isSetup = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSetup)
        {
            Time.timeScale = 1f;
            chooseWeaponView.SetActive(false);
        }
    }

    public void SetupWeapon(WeaponScriptableObject weaponScriptableObject)
    {
        this.weaponScriptableObject = weaponScriptableObject;
        GameObject weapon = Instantiate(weaponScriptableObject.Prefab);
        weapon.transform.SetParent(weaponParent, false);
        WeaponController weaponController = weaponParent.GetComponent<WeaponController>();
        if(weaponScriptableObject.Type == WeaponScriptableObject.TypeWeapon.Melee)
        {
            AnimationEventHelper animationEventHelper = weapon.GetComponent<AnimationEventHelper>();
            animationEventHelper.OnAnimationEventTriggered.AddListener(weaponController.ResetIsAttacking);
            animationEventHelper.OnAttackPeformed.AddListener(weaponController.DetectColliders);
        }
        weaponController.circleOrigin = weapon.transform.Find("CircleOrigin");
        weaponController.animator = weapon.GetComponent<Animator>();
        weaponController.ChooseWeapon(weaponScriptableObject);
        isSetup = true;
    }
}
