using System;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Items : MonoBehaviour
{
    private Vector2 startPos;
    private bool isSelected;
    private bool isInSafeZone;
    [SerializeField, Range(0, 100)] private int damage = 8;
    
    [SerializeField]private Sprite spriteWhenHovering;
    private Sprite sprite;
    private Sprite tmpSprite;
    
    private HealthBar healthBarScript;
    
    private void Start()
    {
        healthBarScript = FindObjectOfType<HealthBar>();
        startPos = transform.position;
        sprite = GetComponent<SpriteRenderer>().sprite;
        tmpSprite = sprite;
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0) && isSelected && isInSafeZone)
        { 
            GameManager.instance.RecupItem();
            isSelected = false;
            gameObject.SetActive(false);
            DOVirtual.DelayedCall(10f, () =>
            { 
                transform.position = startPos;
                gameObject.SetActive(true);
            } );
        } else if (Input.GetMouseButtonUp(0) && isSelected)
        {
            isSelected = false;
        }  
        
        if (isSelected)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
            sprite = tmpSprite;
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

    private void OnMouseOver()
    {
        if (isSelected)
        {
            return;
        }
        sprite = spriteWhenHovering;
    }
}