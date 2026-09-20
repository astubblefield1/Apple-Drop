using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI myText;
    public GameObject control;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameController c = control.GetComponent<GameController>();
        if(c!= null)
        {
            myText.text = "Score: "+c.getScore().ToString();
        }

    }
}
