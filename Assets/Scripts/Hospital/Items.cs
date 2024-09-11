using System;
using UnityEngine;

public class Items : MonoBehaviour
{
    private Vector2 startPos;
    public static event Action OnBodyTouched;
    private bool isSelected;
    public static event Action OnItemOut;
    private Collider2D col;
    private bool isInSafeZone;
    private bool canBeSelected = true;

    private void Start()
    {
        startPos = transform.position;
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (isSelected)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }

        if (Input.GetMouseButtonUp(0) && isSelected)
        {
            isSelected = false;
        }
        else if(Input.GetMouseButtonUp(0) && isSelected && isInSafeZone)
        {
           isSelected = false;
           OnItemOut?.Invoke();
           col.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Body"))
        {
            isSelected = false;
            transform.position = startPos;
            OnBodyTouched?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer.Equals(6))
        {
            isInSafeZone = true;
            canBeSelected = false;
        }
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer.Equals(6))
        {
            isInSafeZone = false;
            canBeSelected = true;
        }
    }

    private void OnMouseDown()
    {
        if (canBeSelected)
        {
            isSelected = true;
        }
    }
}