using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
public class HealthBar : MonoBehaviour
{
    private Slider _slider;
    private int maxHealth = 240;
    private int currentHealth;
    
    [SerializeField, Range(0,100)] private int damage = 20;
    
    private int bonusHealth = 5;


    private void Start()
    {
        _slider = GetComponent<Slider>();
        currentHealth = maxHealth;
        UpdateSlider(currentHealth);
        Loop();
    }

    private void Loop()
    {
        DOVirtual.DelayedCall(1f, () =>
        {
            TakeDamage(1);
            Loop();
        });
    }
    
    
    public void BonusHealth()
    {
        currentHealth += bonusHealth;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateSlider(currentHealth);
    }

  

    private void Update()
    {
       
        if (Input.GetKeyDown("d"))
        {
            TakeDamage(damage);
        }else if (Input.GetKeyDown("p"))
        {
            BonusHealth();
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        UpdateSlider(currentHealth);
    }
    
    
    
    private void UpdateSlider(int health)
    {
        _slider.value = health;
        if (health <= 0)
        {
            _slider.value = 0;
            Application.Quit();
        }
    }
    
}
