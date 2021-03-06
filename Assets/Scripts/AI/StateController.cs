﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [SerializeField]
    private State currentState;
    [SerializeField]
    private State remainState;
    [SerializeField]
    private Stats stats;

    public State CurrentState { get; private set; }
    public State RemainState { get; private set; }
    public Stats Stats => stats;
    public Transform Eyes => GetComponentInChildren<Transform>();
    public MovementAgent MovementAgent => GetComponent<MovementAgent>();
    public FieldOfView FOV => GetComponentInChildren<FieldOfView>();
    public Vector3[] WaypointList => BoardManager.Instance.ShortestPath.GetPath(transform.position, ChaseTarget.position);
    //public List<Vector3> WaypointList => waypointList;
    public int NextWaypoint { get; set; } = 0;
    private bool IsAIActive { get; set; } = false;
    public Transform ChaseTarget { get; set; }


    // Use this for initialization
    void Start()
    {
        CurrentState = currentState;
        RemainState = remainState;
    }

    void Update()
    {
        if (IsAIActive)
        {
            CurrentState.UpdateState(this);
        }
    }

    void OnDrawGizmos()
    {
        if (CurrentState != null && Eyes != null)
        {
            Gizmos.color = CurrentState.SceneGizmoColor;
            Gizmos.DrawWireSphere(Eyes.position, Stats.lookSphereCastRadius);
        }
    }

    public void SetupAI(bool aiActivation)
    {
        IsAIActive = aiActivation;
        if (!IsAIActive)
        {
            MovementAgent.StopMovement();
            ChaseTarget = null;
        }
    }

    public void SetupAI(bool aiActivation, Transform chaseTarget)
    {
        SetupAI(aiActivation);
        ChaseTarget = chaseTarget;
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != RemainState)
        {
            CurrentState = nextState;
        }
    }
}
