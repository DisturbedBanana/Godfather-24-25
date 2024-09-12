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
     private GameObject[] items;

  [SerializeField]  private SpawnItems scriptItem;
    
    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
        scriptItem.OnSpawn += SaveItem;
        image.gameObject.SetActive(false);
        text.text = nbItemsRecup.ToString()+ "/20";
    }

    private void OnDestroy()
    {
        scriptItem.OnSpawn -= SaveItem;

    }

    private void SaveItem()
    {
        items = scriptItem.items;
    }

    public void RecupItem()
    {
        nbItemsRecup++;
        text.text = nbItemsRecup.ToString()+ "/20";
    }

    private void Update()
    {
        if (nbItemsRecup == 20)
        {

            Time.timeScale = 0;
            image.gameObject.SetActive(true);
            foreach (var item in items)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
    
    
}
