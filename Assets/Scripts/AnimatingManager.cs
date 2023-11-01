using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatingManager : MonoBehaviour
{
    private Animator animate;
    public AudioClip[] clips;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        animate = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

   void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            source.clip = clips[0];
            source.Play();
            animate.SetBool("DoorOpen", true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            source.clip = clips[1];
            source.Play();
            animate.SetBool("DoorOpen", false);
        }
    }
}
