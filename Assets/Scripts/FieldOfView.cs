﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]
    private float viewRange = 2f;
    [SerializeField, Range(0, 360)]
    private float viewAngle = 120f;
    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    private LayerMask obstacleMask;

    public float ViewRange => viewRange;
    public float ViewAngle => viewAngle;
    public List<Transform> VisibleTargets { get; private set; } = new List<Transform>();
    public LayerMask TargetMask => targetMask;
    public LayerMask ObstacleMask => obstacleMask;

    private void Start()
    {
        StartCoroutine(FindTargetsWithDelay(.2f));
    }

    private IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private void FindVisibleTargets()
    {
        VisibleTargets.Clear();
        Collider2D[] targetsInViewRange = Physics2D.OverlapCircleAll(transform.position, viewRange, TargetMask);
        foreach (var collider in targetsInViewRange)
        {
            Transform target = collider.transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.up, dirToTarget) < viewAngle / 2f)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                var hit = Physics2D.Raycast(transform.position, dirToTarget, distanceToTarget, ObstacleMask);
                if (hit.transform == null)
                {
                    VisibleTargets.Add(target);
                }
            }
        }
    }

    //public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    //{
    //    if (!angleIsGlobal)
    //    {
    //        angleInDegrees -= transform.eulerAngles.z;
    //    }
    //    return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad) * ViewRange, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad) * ViewRange);
    //}
}
