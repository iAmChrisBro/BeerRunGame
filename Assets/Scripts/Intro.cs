using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public Animator animate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetIntro());
    }

    IEnumerator SetIntro()
    {
        yield return new WaitForSeconds(3);
        animate.SetTrigger("FadeOut");
        SceneManager.LoadScene("Menu");
    }
}
