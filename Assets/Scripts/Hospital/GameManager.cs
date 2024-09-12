using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int nbItemsRecup;
    public static GameManager instance;
    [SerializeField] private TextMeshProUGUI text;
    
    
    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
        text.text = nbItemsRecup.ToString();
    }

    public void RecupItem()
    {
        nbItemsRecup++;
        text.text = nbItemsRecup.ToString();
    }

    private void Update()
    {
        if (nbItemsRecup == 20)
        {
            Time.timeScale = 0;
            Application.Quit();
        }
    }
    
    
}
