using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayer : MonoBehaviour
{
    // CLASS IS NOT NEEDED ANYMORE DELETE 
    public GameObject[] players;
    // Start is called before the first frame update
    void Start()
    {
        int playerIndex = PlayerPrefs.GetInt("selectedPlayer");
        GameObject player = Instantiate(players[playerIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
