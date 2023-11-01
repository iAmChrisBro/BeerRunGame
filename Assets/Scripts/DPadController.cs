using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
/**
 * CLASS IS NOT IN USE ANYMORE
 * 
 **/
public class DPadController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 dir;
    public Animator animate;
    private bool running;

    public float speed;
    public float runSpeed;
   // private float tempSpeed;
    private bool left, right, up, down;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        left = false;
        right = false;
        up = false;
        down = false;
        AnimatePlayer("FrontIdle");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if ((!left && !right) && (!up && !down))
        {     
            dir = Vector2.zero;
        }
        if (left)
        {
            if(running)
            {
                AnimatePlayer("LeftRun");
            }
            else
            AnimatePlayer("LeftWalk");

            dir = Vector2.left;
        }
        else if (right)
        {
            if(running)
            {
                AnimatePlayer("RightRun");
            }
            else
            AnimatePlayer("RightWalk");

            dir = Vector2.right;
        }
        if (up)
        {
            if(running)
            {
                AnimatePlayer("BackRun");
            }
            else
            AnimatePlayer("BackWalk");

            dir = Vector2.up;
        }
        else if (down)
        {
            if (running)
            {
                AnimatePlayer("FrontRun");
            }
            else
            AnimatePlayer("FrontWalk");

            dir = Vector2.down;
        }

        transform.Translate(dir * speed * Time.deltaTime);
    }

    public float getSpeed()
    {
        return speed;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void addSpeed(float speed)
    {
        this.speed += speed;
        
    }

    public void subSpeed(float speed)
    {
        this.speed -= speed;
        
    }

    public void LeftUp()
    {
        AnimatePlayer("LeftIdle");
        left = false;
    }

    public void LeftDown()
    {
        left = true;
    }

    public void RightUp()
    {
        AnimatePlayer("RightIdle");
        right = false;
    }

    public void RightDown()
    {
        right = true;
    }

    public void UpUp()
    {
        AnimatePlayer("BackIdle");
        up = false;
    }

    public void UpDown()
    {
        up = true;
    }

    public void DownUp()
    {
        AnimatePlayer("FrontIdle");
        down = false;
    }

    public void DownDown()
    {
        down = true;
    }

    public void RunDown()
    {
        running = true;
        speed += runSpeed;
    }

    public void RunUp()
    {
        running = false;
        speed -= runSpeed;
    }

    public bool isRunning()
    {
        return this.running;
    }

    private void AnimatePlayer(string name)
    {
        switch(name)
        {
            case "FrontIdle":
                animate.SetBool("FrontIdle", true);
                animate.SetBool("FrontWalk", false);
                animate.SetBool("FrontRun", false);
                animate.SetBool("BackIdle", false);
                animate.SetBool("BackWalk", false);
                animate.SetBool("BackRun", false);
                animate.SetBool("RightIdle", false);
                animate.SetBool("RightWalk", false);
                animate.SetBool("RightRun", false);
                animate.SetBool("LeftIdle", false);
                animate.SetBool("LeftWalk", false);
                animate.SetBool("LeftRun", false);
                break;

            case "FrontWalk":
                animate.SetBool("FrontIdle", false);
                animate.SetBool("FrontWalk", true);
                animate.SetBool("FrontRun", false);
                animate.SetBool("BackIdle", false);
                animate.SetBool("BackWalk", false);
                animate.SetBool("BackRun", false);
                animate.SetBool("RightIdle", false);
                animate.SetBool("RightWalk", false);
                animate.SetBool("RightRun", false);
                animate.SetBool("LeftIdle", false);
                animate.SetBool("LeftWalk", false);
                animate.SetBool("LeftRun", false);
                break;

            case "FrontRun":
                animate.SetBool("FrontIdle", false);
                animate.SetBool("FrontWalk", false);
                animate.SetBool("FrontRun", true);
                animate.SetBool("BackIdle", false);
                animate.SetBool("BackWalk", false);
                animate.SetBool("BackRun", false);
                animate.SetBool("RightIdle", false);
                animate.SetBool("RightWalk", false);
                animate.SetBool("RightRun", false);
                animate.SetBool("LeftIdle", false);
                animate.SetBool("LeftWalk", false);
                animate.SetBool("LeftRun", false);
                break;

            case "BackIdle":
                animate.SetBool("FrontIdle", false);
                animate.SetBool("FrontWalk", false);
                animate.SetBool("FrontRun", false);
                animate.SetBool("BackIdle", true);
                animate.SetBool("BackWalk", false);
                animate.SetBool("BackRun", false);
                animate.SetBool("RightIdle", false);
                animate.SetBool("RightWalk", false);
                animate.SetBool("RightRun", false);
                animate.SetBool("LeftIdle", false);
                animate.SetBool("LeftWalk", false);
                animate.SetBool("LeftRun", false);
                break;

            case "BackWalk":
                animate.SetBool("FrontIdle", false);
                animate.SetBool("FrontWalk", false);
                animate.SetBool("FrontRun", false);
                animate.SetBool("BackIdle", false);
                animate.SetBool("BackWalk", true);
                animate.SetBool("BackRun", false);
                animate.SetBool("RightIdle", false);
                animate.SetBool("RightWalk", false);
                animate.SetBool("RightRun", false);
                animate.SetBool("LeftIdle", false);
                animate.SetBool("LeftWalk", false);
                animate.SetBool("LeftRun", false);
                break;

            case "BackRun":
                animate.SetBool("FrontIdle", false);
                animate.SetBool("FrontWalk", false);
                animate.SetBool("FrontRun", false);
                animate.SetBool("BackIdle", false);
                animate.SetBool("BackWalk", false);
                animate.SetBool("BackRun", true);
                animate.SetBool("RightIdle", false);
                animate.SetBool("RightWalk", false);
                animate.SetBool("RightRun", false);
                animate.SetBool("LeftIdle", false);
                animate.SetBool("LeftWalk", false);
                animate.SetBool("LeftRun", false);
                break;

            case "RightIdle":
                animate.SetBool("FrontIdle", false);
                animate.SetBool("FrontWalk", false);
                animate.SetBool("FrontRun", false);
                animate.SetBool("BackIdle", false);
                animate.SetBool("BackWalk", false);
                animate.SetBool("BackRun", false);
                animate.SetBool("RightIdle", true);
                animate.SetBool("RightWalk", false);
                animate.SetBool("RightRun", false);
                animate.SetBool("LeftIdle", false);
                animate.SetBool("LeftWalk", false);
                animate.SetBool("LeftRun", false);
                break;

            case "RightWalk":
                animate.SetBool("FrontIdle", false);
                animate.SetBool("FrontWalk", false);
                animate.SetBool("FrontRun", false);
                animate.SetBool("BackIdle", false);
                animate.SetBool("BackWalk", false);
                animate.SetBool("BackRun", false);
                animate.SetBool("RightIdle", false);
                animate.SetBool("RightWalk", true);
                animate.SetBool("RightRun", false);
                animate.SetBool("LeftIdle", false);
                animate.SetBool("LeftWalk", false);
                animate.SetBool("LeftRun", false);
                break;

            case "RightRun":
                animate.SetBool("FrontIdle", false);
                animate.SetBool("FrontWalk", false);
                animate.SetBool("FrontRun", false);
                animate.SetBool("BackIdle", false);
                animate.SetBool("BackWalk", false);
                animate.SetBool("BackRun", false);
                animate.SetBool("RightIdle", false);
                animate.SetBool("RightWalk", false);
                animate.SetBool("RightRun", true);
                animate.SetBool("LeftIdle", false);
                animate.SetBool("LeftWalk", false);
                animate.SetBool("LeftRun", false);
                break;

            case "LeftIdle":
                animate.SetBool("FrontIdle", false);
                animate.SetBool("FrontWalk", false);
                animate.SetBool("FrontRun", false);
                animate.SetBool("BackIdle", false);
                animate.SetBool("BackWalk", false);
                animate.SetBool("BackRun", false);
                animate.SetBool("RightIdle", false);
                animate.SetBool("RightWalk", false);
                animate.SetBool("RightRun", false);
                animate.SetBool("LeftIdle", true);
                animate.SetBool("LeftWalk", false);
                animate.SetBool("LeftRun", false);
                break;

            case "LeftWalk":
                animate.SetBool("FrontIdle", false);
                animate.SetBool("FrontWalk", false);
                animate.SetBool("FrontRun", false);
                animate.SetBool("BackIdle", false);
                animate.SetBool("BackWalk", false);
                animate.SetBool("BackRun", false);
                animate.SetBool("RightIdle", false);
                animate.SetBool("RightWalk", false);
                animate.SetBool("RightRun", false);
                animate.SetBool("LeftIdle", false);
                animate.SetBool("LeftWalk", true);
                animate.SetBool("LeftRun", false);
                break;

            case "LeftRun":
                animate.SetBool("FrontIdle", false);
                animate.SetBool("FrontWalk", false);
                animate.SetBool("FrontRun", false);
                animate.SetBool("BackIdle", false);
                animate.SetBool("BackWalk", false);
                animate.SetBool("BackRun", false);
                animate.SetBool("RightIdle", false);
                animate.SetBool("RightWalk", false);
                animate.SetBool("RightRun", false);
                animate.SetBool("LeftIdle", false);
                animate.SetBool("LeftWalk", false);
                animate.SetBool("LeftRun", true);
                break;
        }
    }
}
