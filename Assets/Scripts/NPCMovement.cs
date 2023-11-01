using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform[] moveSpots;
    public float startWaitTime;
    private int index;
    private float waitTime;
    public Animator animate;
    public static float speed;
    public static float hDir, vDir;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        index = 0;
        speed = 25f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAI();
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

            foreach(GameObject npc in npcs)
            {
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), npc.GetComponent<Collider2D>());
            }
        }
    }
}
