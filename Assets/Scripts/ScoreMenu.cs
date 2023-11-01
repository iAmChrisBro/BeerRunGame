using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using DigitalRubyShared;

public class ScoreMenu : MonoBehaviour
{
    private Collider2D col;
    private BoxCollider2D border;
    public TilemapCollider2D tm;
    public GameObject scoreMenuUI, UICanvas, bustedUI;
    public Text beer, time, stealth;
    public static bool passed;
    public GameObject[] stars;
    public int OneStar, TwoStar, ThreeStar;
    public static bool grabbed;

    private int levelCount;
    private string minutes, seconds;
    private float startTime;
    private Transform security;

    // Start is called before the first frame update
    void Start()
    {
        grabbed = false;
        col = GetComponent<Collider2D>();
        border = GetComponent<BoxCollider2D>();
        security = GameObject.FindGameObjectWithTag("Security").GetComponent<Transform>();
        scoreMenuUI.SetActive(false);
        tm.enabled = false;
        border.enabled = true;
        startTime = Time.time;
        passed = false;
        levelCount = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (ScoreManager.count > 0)
        {
            grabbed = true;
            tm.enabled = true;
            border.enabled = false;
        }

        float t = Time.time - startTime;

         minutes = ((int)t / 60).ToString();
         seconds = (t % 60).ToString("f1");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        passed = true;
        security.position = Vector2.MoveTowards(security.position, security.position, 0 * Time.deltaTime);
        //DPadController.speed = 0;
        switch(levelCount)
        {
            case 6: levelCount = 1;
                break;
            case 8:
                levelCount = 2;
                break;
            case 10:
                levelCount = 3;
                break;
            case 12:
                levelCount = 4;
                break;
            case 14:
                levelCount = 5;
                break;
            case 16:
                levelCount = 6;
                break;
            case 18:
                levelCount = 7;
                break;
            case 20:
                levelCount = 8;
                break;
        }
        if (levelCount > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", levelCount);
        }

        PlayerController.levelOver = true;
        SetScoreMenu();
    }

    private void SetScoreMenu()
    {
        string level = levelCount.ToString();

        if (SecurityAI.caught)
        {
            stealth.color = Color.red;
            stealth.text = "NO";
        }
        else
        {
            stealth.color = Color.green;
            stealth.text = "YES";
        }

        if (ScoreManager.count == ThreeStar && !SecurityAI.caught)
        {
            stars[3].SetActive(true);
            PlayerPrefs.SetInt(level, 3);
        }
        else if (ScoreManager.count >= TwoStar && ScoreManager.count <= ThreeStar)
        {
            stars[2].SetActive(true);
            PlayerPrefs.SetInt(level, 2);
        }
        else if (ScoreManager.count >= OneStar && ScoreManager.count < TwoStar)
        {
            stars[1].SetActive(true);
            PlayerPrefs.SetInt(level, 1);
        }
        else
        {
            stars[0].SetActive(true);
            PlayerPrefs.SetInt(level, 0);
        }

        beer.text = ScoreManager.count.ToString();

        time.text = minutes + " : " + seconds;

        scoreMenuUI.SetActive(true);
        bustedUI.SetActive(false);
        UICanvas.SetActive(false);

    }

    public void Levels()
    {
       // DPadController.speed = 3;
        SceneManager.LoadScene("Levels");
    }

     public void Next()
    {
        // DPadController.speed = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EndLevel()
    {
        SceneManager.LoadScene("EndScene");
    }
}
