using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius = 5f;
    [Range(1, 360)] public float angle = 45f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;

    private GameObject playerRef;

    public bool CanSeePlayer { get; private set; }

    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVCheck());
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

        if(SecurityAI.hDir == 0 && SecurityAI.vDir == 0)
            transform.eulerAngles = new Vector3(0, 0, 180);

        if (SecurityAI.hDir == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (SecurityAI.hDir == -1)
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        if (SecurityAI.vDir == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (SecurityAI.vDir == -1)
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

        Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -angle/2);
        Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, angle / 2);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        if(CanSeePlayer && playerRef != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position,playerRef.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

   
}
