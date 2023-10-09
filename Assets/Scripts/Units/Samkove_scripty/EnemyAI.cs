using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 3;
    private float usingSpeed = 0;
    public float nextWaypointDistance = 3;
    public bool isShooter = false;
    public float keepShootingDistance = 0;

    private int sign = 1;

    private PlayerAwerness playerAwarness;

    Path path;
    int currentWaypoint = 0;
    bool reachedEnDOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    private Animator animator;

    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        playerAwarness = GetComponent<PlayerAwerness>();
        InvokeRepeating("UpdatePath", 0f, .5f);
        usingSpeed = speed;
    }

    private void Update()
    {
        if (playerAwarness.AwareOfPlayer && rb.velocity!= Vector2.zero)
        {
            animator.SetFloat("Horizontal", playerAwarness.directionToPlayer.x * sign);
            animator.SetFloat("Vertical", playerAwarness.directionToPlayer.y * sign);
            animator.SetFloat("Speed", new Vector2(playerAwarness.directionToPlayer.x, playerAwarness.directionToPlayer.y).sqrMagnitude);
        }
        else
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", direction.sqrMagnitude);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerAwarness.AwareOfPlayer)
        {
            Move();
        }
        else
        {
            direction = Vector2.zero;
            rb.velocity = direction;
        }
    }
    void UpdatePath()
    {
        if (playerAwarness.AwareOfPlayer && seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Move()
    {
        if (path == null)
            return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEnDOfPath = true;
            return;
        }
        else
        {
            reachedEnDOfPath = false;
        }

        if (isShooter && playerAwarness.enemyToPlayerVector.sqrMagnitude <= keepShootingDistance+1 && playerAwarness.enemyToPlayerVector.sqrMagnitude >= keepShootingDistance-1)
        {
            direction = Vector2.zero;
            rb.velocity = direction;
            sign = 1;
            usingSpeed = speed;
            return;
        }
        else if (isShooter && playerAwarness.enemyToPlayerVector.sqrMagnitude < keepShootingDistance-1)
        {
            sign = -1;
            usingSpeed = speed / 2;
        }
        else
        {
            sign = 1;
            usingSpeed = speed;
        }
        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 velo = sign * direction * usingSpeed; //* Time.deltaTime;

        rb.velocity = velo;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
