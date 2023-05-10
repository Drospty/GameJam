using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnEndReached;
    [SerializeField] private float MoveSpeed = 5f;
    private Waypoint _waypoint;
    private int _currentWaypointIndex = 0;
    private SpriteRenderer _spriteRenderer;
    private EnemyHealth _enemyHealth;

    private void Start()
    {
        _waypoint = FindObjectOfType<Waypoint>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        Rotate();
        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    private Vector3 CurrentPointPosition
    {
        get { return _waypoint.CurrentPosition + _waypoint.Points[_currentWaypointIndex]; }
    }

    private Vector3 LastPointPosition
    {
        get { return _waypoint.CurrentPosition + _waypoint.Points[GetPreviousWaypointIndex()]; }
    }

    public object OnEndReach { get; private set; }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, MoveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        if (CurrentPointPosition.x < LastPointPosition.x)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            return true;
        }
        return false;
    }

    private void UpdateCurrentPointIndex()
    {
        if (_currentWaypointIndex < _waypoint.Points.Length - 1)
        {
            _currentWaypointIndex++;
        }
        else
        {
            EndPointReached();
        }
    }

    private int GetPreviousWaypointIndex()
    {
        if (_currentWaypointIndex > 0)
        {
            return _currentWaypointIndex - 1;
        }
        return _waypoint.Points.Length - 1;
    }

    private void EndPointReached()
    {
        OnEndReached.Invoke(this);
        _enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(gameObject);
    }

    public void StopMovement()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
    public void ResumeMovement()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

}
