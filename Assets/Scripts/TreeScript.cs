using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public float speed = 2;
    private bool goingRight = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (goingRight)
        {
            transform.position = new Vector2(transform.position.x+Time.deltaTime*speed, transform.position.y);
        } else{
        transform.position = new Vector2(transform.position.x-Time.deltaTime*speed, transform.position.y);
        }
        if (Camera.main.WorldToScreenPoint(transform.position).x > Screen.width-125)
        {
            goingRight = false;
        }
         if (Camera.main.WorldToScreenPoint(transform.position).x < 0+125)
        {
            goingRight = true;
        }
    }
}
