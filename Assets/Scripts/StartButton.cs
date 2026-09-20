using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject control;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        GameController c = control.GetComponent<GameController>();
        if (c!= null)
        {
            c.setup();
        }
    } 
}
