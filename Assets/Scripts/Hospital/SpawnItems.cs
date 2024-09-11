using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private Transform[] spawnPoints;


    private void Start()
    {
        SpawnItem(2);
    }
    
    
    private void SpawnItem(int nbItems)
    {
        for (var i = 0; i < nbItems; i++)
        {
            var random = Random.Range(0, items.Length); 
            var pos = new Vector2(spawnPoints[i].position.x, spawnPoints[i].position.y);
            Instantiate(items[random], pos, Quaternion.identity);
        }
    }    
    
}
