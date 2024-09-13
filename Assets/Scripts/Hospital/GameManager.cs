using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int nbItemsRecup;
    public static GameManager instance;
    [SerializeField] private TextMeshProUGUI text;
    
     [SerializeField] public SpriteRenderer image;
     [SerializeField] private SpriteRenderer[] hearts;
     public event Action OnItemRecup;

    [SerializeField]  private SpawnItems scriptItem;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        image.gameObject.SetActive(false);
        text.text = nbItemsRecup.ToString()+ "/20";
    }

    
    
    
    public void RecupItem()
    {
        nbItemsRecup++;
        if (nbItemsRecup % 2 == 0 && nbItemsRecup / 2 - 1 < hearts.Length)
        {
            hearts[nbItemsRecup / 2 - 1].gameObject.SetActive(true);
        }
        text.text = nbItemsRecup+ "/20";
    }

    private void Update()
    {
        if (nbItemsRecup == 20)
        {

            Time.timeScale = 0;
            OnItemRecup?.Invoke();
        }
    }
    
    
}
