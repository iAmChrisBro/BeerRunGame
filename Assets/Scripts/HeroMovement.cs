using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;

public class HeroMovement : MonoBehaviour
{
    public Transform[] moveSpots;
    public float startWaitTime;
    private int index;
    private float waitTime;
    private Animator animate;
    public static float speed;
    public static float hDir, vDir;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animate = GetComponent<Animator>();
        waitTime = startWaitTime;
        index = 0;
        speed = 25f;
    }

    // Update is called once per frame
    void Update()
    {
        if (NPCHero.caught && ScoreManager.count > 0)
            FollowPlayer();
        else
            MoveAI();
    }

    private void FollowPlayer()
    {
        //animate.SetBool("Run", true);
        animate.SetBool("Idle", false);
        animate.SetFloat("moveX", (target.position.x - transform.position.x));
        animate.SetFloat("moveY", (target.position.y - transform.position.y));

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

    }

    void MoveAI()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[index].position, speed * Time.deltaTime);

        float h = Mathf.Clamp(transform.position.x - moveSpots[index].position.x, -1f, 1f);
        float v = Mathf.Clamp(transform.position.y - moveSpots[index].position.y, -1f, 1f);

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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "NPC")
        {
            GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

            foreach (GameObject npc in npcs)
            {
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), npc.GetComponent<Collider2D>());
            }
        }

        if(col.gameObject.tag == "Player")
        {
            PlayerController.Speed = 20f;
            Debug.Log("20");
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            PlayerController.Speed = 25f;
            Debug.Log("25f");
        }
    }
}
