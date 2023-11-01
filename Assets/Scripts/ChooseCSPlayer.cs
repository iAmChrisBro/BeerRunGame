using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCSPlayer : MonoBehaviour
{
    public GameObject Genius, Manik, Stoner;

    void Start()
    {
        int playerIndex = PlayerPrefs.GetInt("selectedPlayer");

        switch (playerIndex)
        {
            case 0:
                Genius.SetActive(true);
                break;
            case 1:
                Manik.SetActive(true);
                break;
            case 2:
                Stoner.SetActive(true);
                break;
        }
    }

}
