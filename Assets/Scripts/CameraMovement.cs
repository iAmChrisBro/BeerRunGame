using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    private Transform target;
    public Vector3 offset;
    public Vector2 minPosition, maxPosition;
    public GameObject UI;
    public float smoothing;
    private ChoosePlayer player;
    public GameObject[] players;

    void Start()
    {
        Instantiate(players[player.getPlayer()]);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(SetUI());
    }

    private void Update()
    {
        transform.position = target.position + offset;

        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing) + offset;
        }
    }

    IEnumerator SetUI()
    {
        UI.SetActive(false);
        yield return new WaitForSeconds(5);
        UI.SetActive(true);
        
    }
}
