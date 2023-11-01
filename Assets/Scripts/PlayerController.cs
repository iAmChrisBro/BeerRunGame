//
// Fingers Gestures
// (c) 2015 Digital Ruby, LLC
// http://www.digitalruby.com
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DigitalRubyShared
{
    /// <summary>
    /// DPad demo script
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// First dpad script
        /// </summary>
        [Tooltip("Fingers DPad Script")]
        public FingersDPadScript DPadScript;

        /// <summary>
        /// First mover
        /// </summary>
        [Tooltip("Object to move with the dpad")]
        private GameObject Mover;

        /// <summary>
        /// Move speed
        /// </summary>
        [Tooltip("Units per second to move the player with dpad")]
        public static float Speed = 25f;

        /// <summary>
        /// Run speed
        /// </summary>
        [Tooltip("Units per second to run the player with button")]
        public float runSpeed = 50.0f;

        //[Tooltip("Whether dpad moves to touch start location")]
        //public bool MoveDPadToGestureStartLocation;

        private Vector3 startPos;
        private Vector3 tempPos;

        public GameObject dPad;
        private int levelCount;

        public GameObject[] players;
        public float x, y, z;
        private Animator animate;
        private bool isRunning = false;
        private bool left, right, up, down;
        public static bool levelOver = false;
        
        private Rigidbody2D rb;
        void Start()
        {
            Speed = 25f;
            ScoreManager.multiplier = 0f;
            levelOver = false;
            levelCount = SceneManager.GetActiveScene().buildIndex;
            
            switch (levelCount)
            {
                case 6:
                    levelCount = 1;
                    break;
                case 8:
                    levelCount = 2;
                    break;
                case 10:
                    levelCount = 3;
                    break;
                case 12:
                    levelCount = 4;
                    break;
                case 14:
                    levelCount = 5;
                    break;
                case 16:
                    levelCount = 6;
                    break;
                case 18:
                    levelCount = 7;
                    break;
                case 20:
                    levelCount = 8;
                    break;
            }

            if (levelCount > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", levelCount);
            }
        }

        void Awake()
        {
            startPos = new Vector3(x, y, z);
            tempPos = (Vector3)startPos;
            int playerIndex = PlayerPrefs.GetInt("selectedPlayer");
            Mover = (GameObject)Instantiate(players[playerIndex], startPos, Quaternion.identity);
            animate = Mover.GetComponent<Animator>();
            rb = Mover.GetComponent<Rigidbody2D>();

            DPadScript.DPadItemTapped = DPadTapped;
            DPadScript.DPadItemPanned = DPadPanned;
            //DPadScript.MoveDPadToGestureStartLocation = MoveDPadToGestureStartLocation;
            //DPadScript2.MoveDPadToGestureStartLocation = MoveDPadToGestureStartLocation;
        }

        void Update()
        {
            //Debug.Log(Speed);
            if (levelOver)
            {
                Mover.transform.position += Vector3.zero * 25 * Time.deltaTime;
                animate.SetFloat("VWalk", -1);
            }

            if(SecurityAI.stopPlayer)
            {
                Mover.transform.position += Vector3.zero * Time.deltaTime;
                animate.SetBool("Idle", true);
            }
            else
            {

                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");

                if(h > 0 || h < 0 || v > 0 || v < 0)
                {
                    dPad.SetActive(false);
                }

                Vector3 movement = new Vector3(h, v, 0);

                if (Input.GetKey(KeyCode.Space))
                {
                    if (CheckCollision.check)
                        Speed = 25f;
                    else
                    {
                        Speed = (int)runSpeed + ScoreManager.multiplier;
                        animate.SetBool("Run", true);
                    }
                }
                else
                {
                    Speed = 25 + ScoreManager.multiplier;
                    animate.SetBool("Run", false);
                }

                if (movement.x == 0 && movement.y == 0)
                {
                    animate.SetBool("Idle", true);
                }
                else
                {
                    animate.SetBool("Idle", false);
                    animate.SetFloat("HWalk", movement.x);
                    animate.SetFloat("VWalk", movement.y);
                }

                movement = movement.normalized * Speed * Time.deltaTime;

                Mover.transform.position += movement;
            }
        }

        private void DPadTapped(FingersDPadScript script, FingersDPadItem item, TapGestureRecognizer gesture)
        {
            dPad.SetActive(true);

            float dirX = 0;
            float dirY = 0;

            GameObject mover = null;
            if (script == DPadScript)
            {
                mover = Mover;
            }
            Vector3 pos = mover.transform.position;
            float move = Speed * Time.deltaTime;
            if ((item & FingersDPadItem.Right) != FingersDPadItem.None)
            {
                pos.x += move;
                dirX = 1;
            }
            if ((item & FingersDPadItem.Left) != FingersDPadItem.None)
            {
                pos.x -= move;
                dirX = -1;
            }
            if ((item & FingersDPadItem.Up) != FingersDPadItem.None)
            {
                pos.y += move;
                dirY = 1;
            }
            if ((item & FingersDPadItem.Down) != FingersDPadItem.None)
            {
                pos.y -= move;
                dirY = -1;
            }
            
            animate.SetBool("Idle", true);
            animate.SetFloat("HWalk", dirX);
            animate.SetFloat("VWalk", dirY);
            mover.transform.position = pos;
        }

        private void DPadPanned(FingersDPadScript script, FingersDPadItem item, PanGestureRecognizer gesture)
        {
            float dirX = 0;
            float dirY = 0;
            GameObject mover = null;
            if (script == DPadScript)
            {
                mover = Mover;
            }
            Vector3 pos = mover.transform.position;
 
            float move = Speed * Time.deltaTime;

            if ((item & FingersDPadItem.Right) != FingersDPadItem.None)
            {
                pos.x += move;
                dirX = 1;
            }
            if ((item & FingersDPadItem.Left) != FingersDPadItem.None)
            {
                pos.x -= move;
                dirX = -1;
                
            }
            if ((item & FingersDPadItem.Up) != FingersDPadItem.None)
            {
                pos.y += move;
                dirY = 1;
            }
            if ((item & FingersDPadItem.Down) != FingersDPadItem.None)
            {
                pos.y -= move;
                dirY = -1;
            }

            if (isRunning)
                animate.SetBool("Run", true);
            else
                animate.SetBool("Run", false);

            animate.SetBool("Idle", false);
            animate.SetFloat("HWalk", dirX);
            animate.SetFloat("VWalk", dirY);


            mover.transform.position = pos;
        }

        public void RunDown()
        {
            Speed += runSpeed;
            isRunning = true;
        }

        public void RunUp()
        {
            Speed -= runSpeed;
            isRunning = false;
        }
    }
}
