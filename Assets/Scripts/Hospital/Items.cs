using System;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Items : MonoBehaviour
{
    private Vector2 startPos;
    private bool isSelected;
    private bool isInSafeZone;
    private GameObject[] items;    
    [SerializeField, Range(0, 100)] private int damage = 3;
    private SpawnItems scriptSpawnItems;
    
    private HealthBar healthBarScript;
    
    private void Start()
    {
        healthBarScript = FindObjectOfType<HealthBar>();
        scriptSpawnItems = FindObjectOfType<SpawnItems>();
        items = scriptSpawnItems.items;
        startPos = transform.position;
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0) && isSelected && isInSafeZone)
        { 
            healthBarScript.BonusHealth();
            isSelected = false;
            gameObject.SetActive(false);
            DOVirtual.DelayedCall(10f, () =>
            { 
               var random = Random.Range(0, items.Length);
               Instantiate(items[random], startPos, Quaternion.identity);
               Destroy(gameObject);
            } );
        } else if (Input.GetMouseButtonUp(0) && isSelected)
        {
            isSelected = false;
        }  
        
        if (isSelected)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Body"))
        {
            healthBarScript.TakeDamage(damage);
            isSelected = false;
            transform.position = startPos;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer.Equals(6))
        {
            isInSafeZone = true;
        }
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer.Equals(6))
        {
            isInSafeZone = false;
        }
    }

    private void OnMouseDown()
    {
        isSelected = true;
        
    }
}