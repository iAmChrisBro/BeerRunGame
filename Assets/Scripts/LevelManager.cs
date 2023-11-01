using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject[] lockedLevels;
    public GameObject[] levels;
    public GameObject[] stars;
    public GameObject[] pos;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        int levelAt = PlayerPrefs.GetInt("levelAt");


        for (int i = 1; i <= levels.Length; i++)
        {
            if(i <= levelAt)
            {
                lockedLevels[i-1].SetActive(false);
                levels[i-1].SetActive(true);
                Transform startPos = pos[i-1].GetComponent<Transform>();

                switch (i)
                {
                    case 1:
                        Instantiate(stars[PlayerPrefs.GetInt("1",0)], startPos, false);
                        break;
                    case 2:
                        Instantiate(stars[PlayerPrefs.GetInt("2",0)], startPos, false);
                        break;
                    case 3:
                        Instantiate(stars[PlayerPrefs.GetInt("3",0)], startPos, false);
                        break;
                    case 4:
                        Instantiate(stars[PlayerPrefs.GetInt("4",0)], startPos, false);
                        break;
                    case 5:
                        Instantiate(stars[PlayerPrefs.GetInt("5",0)], startPos, false);
                        break;
                    case 6:
                        Instantiate(stars[PlayerPrefs.GetInt("6",0)], startPos, false);
                        break;
                    case 7:
                        Instantiate(stars[PlayerPrefs.GetInt("7",0)], startPos, false);
                        break;
                    case 8:
                        Instantiate(stars[PlayerPrefs.GetInt("8",0)], startPos, false);
                        break;
                }
            }
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
