using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;

public class CheckCollision : MonoBehaviour
{
    public static bool check;
    void Start()
    {
        check = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Foreground")
        {
            check = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        check = false;
    }
}
