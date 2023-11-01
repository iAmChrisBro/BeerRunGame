using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FollowCamera : MonoBehaviour
{
    private Transform player;

    public Vector3 offset;
    public Vector3 min, max;
    public float smoothFactor;
    public GameObject[] players;
    public GameObject UI;

    [SerializeField]
    private float pX, pY;
    public float smoothX, smoothY;
    private Vector2 currentVelocity;

    /*public BoxCollider2D mapBounds;

    private float xMin, xMax, yMin, yMax;
    private float camY, camX;
    private float camOrthsize;
    private float cameraRatio;
    private Camera mainCam;*/

    void Start()
    {
        int playerIndex = PlayerPrefs.GetInt("selectedPlayer");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

       /* Physics.IgnoreCollision(player.GetComponent<Collider>(), mapBounds.GetComponent<Collider>());
        xMin = mapBounds.bounds.min.x;
        xMax = mapBounds.bounds.max.x;
        yMin = mapBounds.bounds.min.y;
        yMax = mapBounds.bounds.max.y;
        mainCam = GetComponent<Camera>();
        camOrthsize = mainCam.orthographicSize;
        cameraRatio = (xMax + camOrthsize) / 2.0f;*/

        //StartCoroutine(SetUI());
    }

    private void FixedUpdate()
    {
        Follow();

        /*camY = Mathf.Clamp(player.position.y, yMin + camOrthsize, yMax - camOrthsize);
        camX = Mathf.Clamp(player.position.x, xMin + cameraRatio, xMax - cameraRatio);
        this.transform.position = new Vector3(camX, camY, this.transform.position.z);*/
    }

    void Follow()
    {

        pX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref currentVelocity.x, smoothX);
        pY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref currentVelocity.y, smoothY);

        transform.position = new Vector3(pX, pY, transform.position.z);


        /*Vector3 target = player.position + offset;

         Vector3 bounds = new Vector3(
             Mathf.Clamp(target.x, min.x, max.x),
             Mathf.Clamp(target.y, min.y, max.y),
             Mathf.Clamp(target.z, min.z, max.z));

         Vector3 smoothPosition = Vector3.Lerp(transform.position, bounds, smoothFactor * Time.fixedDeltaTime);
         transform.position = smoothPosition;*/
    }

    IEnumerator SetUI()
    {
        UI.SetActive(false);
        yield return new WaitForSeconds(5);
        UI.SetActive(true);

    }
}
