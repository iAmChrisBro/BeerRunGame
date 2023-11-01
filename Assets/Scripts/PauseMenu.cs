using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DigitalRubyShared;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;

    public GameObject pauseMenuUI;
    public GameObject all;
    private DPadController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<DPadController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGamePaused)
            Paused();

        else
        {
            Resume();
        }
            
    }

    public void Paused()
    {
       all.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Resume()
    {
        all.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void Restart()
    {
        // player.setSpeed(20);
        PlayerController.levelOver = false;
        Debug.Log("Pressed");
        isGamePaused = false;
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        //player.setSpeed(20);
        isGamePaused = false;
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene(1);
    }
}
