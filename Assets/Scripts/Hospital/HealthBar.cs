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
    private int maxHealth = 100;
    private int currentHealth;
    
    [SerializeField, Range(0,100)] private int damage = 10;
    
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
        DOVirtual.DelayedCall(2f, () =>
        {
            TakeDamage(2);
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
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 0;
            }else
            {
                Time.timeScale = 1;
            }
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
