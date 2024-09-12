using System;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using Random = UnityEngine.Random;

public class Items : MonoBehaviour
{
    private Vector2 startPos;
    private bool isSelected;
    [SerializeField, Range(0, 100)] private int damage = 8;
    
    [SerializeField]private Sprite spriteWhenHovering;
    private Sprite sprite;
    private Sprite tmpSprite;
    private Texture2D cursor;
    [SerializeField] private Texture2D newCursor;
    
    private HealthBar healthBarScript;
    
    private void Start()
    {
        healthBarScript = FindObjectOfType<HealthBar>();
        startPos = transform.position;
        sprite = GetComponent<SpriteRenderer>().sprite;
        tmpSprite = sprite;
        cursor = PlayerSettings.defaultCursor;
    }
    
    private void Update()
    {
        
        if (Input.GetMouseButtonUp(0) && isSelected)
        {
            isSelected = false;
        }  
        
        if (isSelected)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x + mousePos.x, transform.position.y + mousePos.y, transform.position.z);
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
            isSelected = false;
            gameObject.SetActive(false);
            DOVirtual.DelayedCall(10f, () =>
            {
                transform.position = startPos;
                gameObject.SetActive(true);
            });
        }
    }
    

    private void OnMouseDown()
    {
        isSelected = true;
        
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = tmpSprite;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseOver()
    {
        if (isSelected)
        {
            return;
        }
        GetComponent<SpriteRenderer>().sprite = spriteWhenHovering;
        Cursor.SetCursor(newCursor, new Vector2(transform.position.x + 2f, transform.position.y - 2f), CursorMode.Auto);
    }
}