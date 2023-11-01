using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public GameObject button;
    public Animator animate;
    public string scene;
    // Start is called before the first frame update
    void Start()
    {
        animate.SetTrigger("FadeIn");
        button.SetActive(false);
        StartCoroutine(SetButton());
        StartCoroutine(SetScene(scene));
    }

    IEnumerator SetButton()
    {
        yield return new WaitForSeconds(3);
        button.SetActive(true);
    }

    IEnumerator SetScene(string scene)
    {
        yield return new WaitForSeconds(15);
        animate.SetTrigger("FadeOut");
        SceneManager.LoadScene(scene);
    }

    public void Skip()
    {
        SceneManager.LoadScene("ChoosePlayer");
    }

}
