using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasketScript : MonoBehaviour
{
    private Vector2 mousePos; 
    public float offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mousePos;
    }
    void OnMousePos(InputValue value)
    {
        mousePos = value.Get<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos = new Vector2(mousePos.x+offset, -3.97f);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(mousePos);
        if(screenPos.x > Screen.width)
          {
            screenPos = new Vector2(screenPos.x%Screen.width, screenPos.y);
            mousePos = Camera.main.ScreenToWorldPoint(screenPos);
        }
        if(screenPos.x < 0)
        {
            screenPos = new Vector2(Screen.width + screenPos.x%-Screen.width, screenPos.y);
            mousePos = Camera.main.ScreenToWorldPoint(screenPos);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        Apple a = collision.GetComponent<Apple>();
        if (a != null)
        {
            a.onCatch();
        }
    }
    public void closestOne()
    {
        Destroy(gameObject);
    }
}
