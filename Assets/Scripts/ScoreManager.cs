using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using DigitalRubyShared;

public class ScoreManager : MonoBehaviour
{
    public Text beerCount;
    public GameObject grabText, dropText;
    public GameObject grabButton, dropButton;
    public static int count;
    private bool grabbed, grabable;
    Collider2D col;
    private int space;
    private float tempSpeed;
    private DPadController player;
    public static float multiplier = 0f;
    private AudioSource soundEffect;
    public AudioClip[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        soundEffect = this.GetComponent<AudioSource>();
        count = 0;
        space = 12;
        col = GetComponent<Collider2D>();
       // player = GameObject.FindGameObjectWithTag("Player").GetComponent<DPadController>();
       // tempSpeed = player.getSpeed();
        grabbed = false;
        dropButton.SetActive(false);
        grabButton.SetActive(false);
        grabText.SetActive(false);
        dropText.SetActive(false);
    }

    void Update()
    {
        if (count < 1)
        {
            dropButton.SetActive(false);
            dropText.SetActive(false);
        }

        if(grabable)
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                GrabDown();
                UpdateText();
            }
        }

        if (Input.GetKeyUp(KeyCode.E) && count > 0)
            Drop();

        if (grabbed)
        {
            UpdateText();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            grabText.SetActive(true);
            grabButton.SetActive(true);
            grabable = true;

            if (count > 0)
            {
                dropButton.SetActive(true);
                dropText.SetActive(true);
            }
            else
            {
                dropButton.SetActive(false);
                dropText.SetActive(false);
            }

            if (count == space)
            {
                grabButton.SetActive(false);
                grabText.SetActive(false);
            }
        }
        
    }

    void UpdateText()
    {
        beerCount.text = count.ToString() + " /" + space;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            grabButton.SetActive(false);
            grabText.SetActive(false);
            grabable = false;
            if (count <= space)
            {
                grabButton.SetActive(false);
                grabText.SetActive(false);
            }
            if (count > 0)
            {
                dropButton.SetActive(true);
                dropText.SetActive(true);
            }
            else
            {
                dropButton.SetActive(false);
                dropText.SetActive(false);
            }
        }
    }

    public void GrabDown()
    {
        if (count == space)
        {
            grabbed = false;
            grabButton.SetActive(false);
            grabText.SetActive(false);
        }
        else
        {
            // Decrease speed when player gets beer but only running speed
            // Walking speed can stay the same or decrease by [1,2]
            //player.subSpeed(multiplier * space);
            //PlayerController.Speed -= 0.5f;
            soundEffect.clip = sounds[0];
            soundEffect.Play();
            multiplier -= 1f;
            grabbed = true;
            count++;
        }
    }

    public void Drop()
    {

        //DPadController.speed += 0.01f * space;
        // Increase speed when player drops beer
        //player.addSpeed(multiplier * space);
        multiplier += 1f;

        if (multiplier < 0)
            multiplier = 0f;
        count--;
        soundEffect.clip = sounds[1];
        soundEffect.Play();
        UpdateText();
        //PlayerController.Speed += 0.5f;
    }

}
