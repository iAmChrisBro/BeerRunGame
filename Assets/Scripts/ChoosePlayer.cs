using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoosePlayer : MonoBehaviour
{
    public GameObject t1, t2, t3;
    public GameObject[] players;
    private int playerIndex;

    public int getPlayer()
    {
        return playerIndex;
    }

    void Awake()
    {
        ChooseGenius();
    }

    public void LoadGame()
    {
        PlayerPrefs.SetInt("selectedPlayer", playerIndex);

        SceneManager.LoadScene("CSLevel1");
    }

    public void ChooseGenius()
    {
        playerIndex = 0;
 
        t1.SetActive(true);
        t2.SetActive(false);
        t3.SetActive(false);
    }

    public void ChooseManik()
    {
        playerIndex = 1;
        
        t1.SetActive(false);
        t2.SetActive(true);
        t3.SetActive(false);
    }

    public void ChooseStoner()
    {
        playerIndex = 2;
        
        t1.SetActive(false);
        t2.SetActive(false);
        t3.SetActive(true);
    }
}
