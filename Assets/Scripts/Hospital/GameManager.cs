using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int nbItemsRecup;
    public static GameManager instance;
    
    private void Awake()
    {
        instance = this;
    }

    public void RecupItem()
    {
        nbItemsRecup++;
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
