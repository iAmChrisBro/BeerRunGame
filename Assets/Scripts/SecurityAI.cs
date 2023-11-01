using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.Diagnostics;
using DigitalRubyShared;


/*
 * 
 * fix animation for when caught
 * fix idle animation
 */
public class SecurityAI : MonoBehaviour
{
    public GameObject caughtUI, UICanvas;
    public Transform[] moveSpots;
    public float startWaitTime;
    public static float speed;
    public Transform ray;
    private int index;
    private float waitTime;
    private Transform target;
    public static bool caught;
    public float fieldOfViewLen;
    public Animator animate;
    public static bool stopPlayer;
    public GameObject symbol;
    private int countSymbol = 0;
    public static float hDir, vDir;
    public FieldOfView fov;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        caughtUI.SetActive(false);
        waitTime = startWaitTime;
        index = 0;
        speed = 20f;
        fieldOfViewLen = 50f;
        caught = false;
        stopPlayer = false;
        symbol.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (fov.CanSeePlayer && ScoreManager.count > 0 && countSymbol != 1)
        {
            caught = true;
            StartCoroutine(SymbolOn());
        }
        if(ScoreMenu.passed)
            StopFollow();

        if (caught)
            FollowPlayer();
        else
            MoveAI();

    }

    IEnumerator SymbolOn()
    {
        countSymbol = 1;
        symbol.SetActive(true);
        yield return new WaitForSeconds(3);
        symbol.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player") && caught)
        {
            // Speed of Security
            speed = 0;
            caughtUI.SetActive(true);
            UICanvas.SetActive(false);

            // Slows down Player in PlayerController
            stopPlayer = true;
        }

        if (col.gameObject.tag == "NPC")
        {
            GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

            foreach (GameObject npc in npcs)
            {
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), npc.GetComponent<Collider2D>());
            }
        }
    }

    void MoveAI()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[index].position, speed * Time.deltaTime);

        float h = Mathf.Clamp(transform.position.x - moveSpots[index].position.x,-1f,1f);
        float v = Mathf.Clamp(transform.position.y - moveSpots[index].position.y,-1f,1f);

        hDir = h;
        vDir = v;

        animate.SetFloat("moveX", h);
        animate.SetFloat("moveY", v);

        if (Vector2.Distance(transform.position, moveSpots[index].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                animate.SetBool("Idle", false);
                index++;        
                
                if (index == moveSpots.Length) index = 0;
                waitTime = startWaitTime;
            }
            else
            {
                animate.SetBool("Idle", true);
                waitTime -= Time.deltaTime;
            }
        }
    }


    private void FollowPlayer()
    {
        animate.SetBool("Run", true);
        animate.SetBool("Idle", false);
        animate.SetFloat("moveX", (target.position.x - transform.position.x));
        animate.SetFloat("moveY", (target.position.y - transform.position.y));

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (CheckCollision.check)
            speed = 25f;
        else
            speed = 50f;

    }

    private void StopFollow()
    {
        speed = 0;
        caught = false;
    }
    
}