using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] public GameObject[] items;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField, Range(0, 20)] private int nb = 4;


    private void Start()
    {
        SpawnItem(nb);
    }
    
    
    private void SpawnItem(int nbItems)
    {
        for (var i = 0; i < nbItems; i++)
        {
            var pos = new Vector2(spawnPoints[i].position.x, spawnPoints[i].position.y);
            Instantiate(items[i], pos, items[i].transform.rotation);
        }
    }    
    
}
