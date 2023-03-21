using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIhealth : MonoBehaviour
{
    public Slider mSlider;
    public Color low;
    public Color ligh;
    public Vector3 offset;
    public BossHealth bossHealth;

    public void SetHealth(float health , float maxHealth)
    {
        mSlider.gameObject.SetActive(health < maxHealth);
        mSlider.value = health;
        mSlider.maxValue = maxHealth;
        mSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, ligh, mSlider.normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {
        mSlider.value = bossHealth.health;
        mSlider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
