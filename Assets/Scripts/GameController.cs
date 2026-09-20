using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int score;
    public int baskCount = 3;
    public GameObject basket;
    public GameObject tree;
    public GameObject floor;
    public GameObject regApple;
    public GameObject goldenApple;
    public GameObject sign;
    public GameObject startButton;
    // I am aware that there is a more efficient way to do this, however, I'm lazy!
    public GameObject scoreText;
    public GameObject highScoreText;
    public GameObject introText;
    public GameObject startText;
    public GameObject retryText;
    private int highScore = 0;
    private int nextUpdSpeed = 10;
    private int nextUpdDropRate = 10;
    private List<Vector3> applePositions = new List<Vector3>();
    private float interval = 3f;
    private float curInter = 3f;
    private bool ready = false;
    private List<GameObject> baskets = new List<GameObject>();
    private List<GameObject> activeApples = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // -----------------~** Game Setup **~-----------------
    void Start()
    {
        applePositions.Add(new Vector3(-1.88f, 2.06f,0f));
        applePositions.Add(new Vector3(1.44f, -.309f, 0f));
        applePositions.Add(new Vector3(-1.09f, -.420f,0f));
        applePositions.Add(new Vector3(-.13f, 1.48f,0f));
        applePositions.Add(new Vector3(2.2f, 2.4f, 0f));
        sign.SetActive(true);
        startButton.SetActive(true);
        highScore = 0;
        renderFloor();
        retryText.SetActive(false);
        startText.SetActive(true);
        highScoreText.SetActive(false);
        scoreText.SetActive(false);
    }

    private void renderFloor()
    {
        for(int i=0; i<Screen.width; i += 32)
        {
            Vector2 pos = new Vector2(i, 20);
            pos = Camera.main.ScreenToWorldPoint(pos);
            Instantiate(floor, pos, Quaternion.identity);
        }
    }
    public void setup()
    {
        score = 0;
        makeBasket(2f);
        makeBasket(1f);
        makeBasket(3f);
        applePositions.ForEach(createApple);
        tree.SetActive(true);
        sign.SetActive(false);
        TreeScript t = tree.GetComponent<TreeScript>();
        if (t != null)
        {
            t.speed = 2;
        }
        curInter = 3f;
        interval = 3f;
        nextUpdSpeed = 10;
        nextUpdDropRate = 10;
        startButton.SetActive(false);
        ready = true;
        scoreText.SetActive(true);
        introText.SetActive(false);
        startText.SetActive(false);
        retryText.SetActive(false);
        highScoreText.SetActive(true);
    }


    // -----------------~** Apple Management **~-----------------
    private void createApple(Vector3 pos)
    {
        GameObject app = Instantiate(getAppleType(), pos, Quaternion.identity);
        app.SetActive(true);
        activeApples.Add(app);
    }
    private GameObject getAppleType()
    {
        if(score<10){
            return regApple;
        }
        else
        {
            int ra = UnityEngine.Random.Range(0,5);
            if (ra < 4)
            {
                return regApple;
            }
            else
            {
                return goldenApple;
            }
        }
    }

    private void dropRandomApple()
    {
        int ri = UnityEngine.Random.Range(0,5);
        GameObject app = activeApples[ri];
        activeApples[ri] = Instantiate(getAppleType(), applePositions[ri], Quaternion.identity);
        activeApples[ri].SetActive(true);
        Apple a = app.GetComponent<Apple>();
        if (a != null)
        {
            a.drop();
        }
        
    }

    // -----------------~** Basket Management **~-----------------


    public void destroyBasket(float i)
    {
        if(ready){
            int minPos = -1;
            float min = 100f; // I know this is bad programming practice and I should set it to the max value but idc, it's not gonna be larger than this
            float baskDist;
            for(int j = 0; j<baskCount; j++)
            {
                baskDist = Math.Abs(baskets[j].transform.position.x-i);
                if (baskDist < min)
                {
                    minPos = j;
                    min = baskDist;
                }
            }
            baskCount--;
            GameObject toDelete = baskets[minPos];
            BasketScript b = toDelete.GetComponent<BasketScript>();
            if(b != null)
            {
                b.closestOne();
            }
            if(minPos == -1)
            {
                Debug.Log("Check this!!!!!");
            }else{
                baskets.RemoveAt(minPos);
            }
            if(baskCount <= 0)
            {
                gameOver();
            }
        }
    }
    private void makeBasket(float i)
    {
        GameObject bask = Instantiate(basket);
        BasketScript s = bask.GetComponent<BasketScript>();
        Vector2 temp = new Vector2(0f, Screen.width/3*i);
        temp = Camera.main.ScreenToWorldPoint(temp);
        s.offset = temp.y;
        bask.SetActive(true);
        baskets.Add(bask);
    }

    // -----------------~** Game Management **~-----------------

    private void gameOver()
    {
        activeApples.ForEach(Destroy);
        activeApples.Clear();
        sign.SetActive(true);
        startButton.SetActive(true);
        ready = false;
        tree.SetActive(false);
        retryText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (ready){
            curInter -=Time.deltaTime;
            if (curInter < 0)
            {
                dropRandomApple();
                curInter = interval;
            }
        }
    }
    public void updateScore(int i)
    {
        score +=i;
        nextUpdSpeed -=i;
        if(nextUpdSpeed <= 0)
        {
            TreeScript t = tree.GetComponent<TreeScript>();
            if (t != null)
            {
                t.speed+=.3f;
            }
            nextUpdSpeed = 10;
        }
        if (nextUpdDropRate <= 0)
        {
            interval -= .2f;
            nextUpdDropRate = 10;
        }
        if(score> highScore)
        {
            highScore = score;
        }

    }
    public int getScore()
    {
        return score;
    }
    public int getHighScore()
    {
        return highScore;
    }
}
