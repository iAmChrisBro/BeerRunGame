using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] clips;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        Invoke("EndSound", time);
    }

    void EndSound()
    {
        source.Stop();
        source.clip = clips[0];
        source.Play();
    }

    void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void MenuButton()
    {
        source.Stop();
        source.clip = clips[0];
        source.Play();
        Invoke("Menu", 2);
    }



}
