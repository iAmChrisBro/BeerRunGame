using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHero : MonoBehaviour
{
    public float radius = 5f;
    [Range(1, 360)] public float angle = 45f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;
    public GameObject symbol;
    private int countSymbol = 0;
    public static bool caught;
    private Transform target;

    private GameObject playerRef;

    public static bool CanSeePlayer { get; private set; }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        caught = false;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        symbol.SetActive(false);
        StartCoroutine(FOVCheck());
    }

    void Update()
    {
        if (CanSeePlayer && ScoreManager.count > 0 && countSymbol != 1)
        {
            caught = true;
            StartCoroutine(SymbolOn());
        }
    }

    IEnumerator SymbolOn()
    {
        countSymbol = 1;
        symbol.SetActive(true);
        yield return new WaitForSeconds(3);
        symbol.SetActive(false);
    }

    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FOV();
        }
    }

    private void FOV()
    {

        if (NPCMovement.hDir == 0 && NPCMovement.vDir == 0)
            transform.eulerAngles = new Vector3(0, 0, 180);

        if (NPCMovement.hDir == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (NPCMovement.hDir == -1)
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        if (NPCMovement.vDir == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (NPCMovement.vDir == -1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if (rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                    CanSeePlayer = true;
                else
                    CanSeePlayer = false;
            }
            else
                CanSeePlayer = false;
        }
        else if (CanSeePlayer)
            CanSeePlayer = false;

    }

    private void OnDrawGizmos()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");

        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -angle / 2);
        Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, angle / 2);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        if (CanSeePlayer && playerRef != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerRef.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }


}
