using UnityEngine;

public class GoldenApple : MonoBehaviour, Apple
{
    public Vector2 startPos;
    private bool unset = true;
    private bool falling = false;
    public GameObject tree;
    public GameObject control;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (unset)
        {
            startPos = transform.position;
            unset = false;
        }
        if (!falling)
        {
            transform.position = new Vector2(startPos.x +tree.transform.position.x,startPos.y);
        } else
        {
            float fallSpeed = 2;
            TreeScript t = tree.GetComponent<TreeScript>();
            if(t != null)
            {
                fallSpeed = t.speed+.25f;
            }
            transform.position = new Vector2(transform.position.x, transform.position.y-fallSpeed*Time.deltaTime);
            if (Camera.main.WorldToScreenPoint(transform.position).y < 0)
            {
                GameController c = control.GetComponent<GameController>();
                if(c!= null)
                {
                    c.destroyBasket(transform.position.x);
                    Destroy(gameObject);
                }
            }
        }

        
    }
    public void onCatch()
    {
        GameController c = control.GetComponent<GameController>();
        if (c!= null)
        {
            c.updateScore(2);
        }
        Destroy(gameObject);
    }
    public void fall()
    {
        
    }
    public void drop()
    {
        falling = true;
    }
}