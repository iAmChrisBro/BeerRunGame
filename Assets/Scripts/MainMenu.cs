using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// Beer Run Game
public class MainMenu : MonoBehaviour
{

   public void PlayGame()
    {
        SceneManager.LoadScene("StoryBoard");
    }

    public void Levels()
    {
        SceneManager.LoadScene("Levels");
    }
}
