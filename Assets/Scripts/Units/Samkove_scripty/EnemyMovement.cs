using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D rb = null;

    [SerializeField]
    private PlayerAwerness playerAwarness;
    private Animator animator;
    private Vector2 targerDirection;


    private void Update()
    {
        animator.SetFloat("Horizontal", targerDirection.x);
        animator.SetFloat("Vertical", targerDirection.y);
        animator.SetFloat("Speed", 1);
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAwarness = GetComponentInChildren<PlayerAwerness>();
        animator = GetComponent<Animator>();

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        SetVelocity();
    }


    private void UpdateTargetDirection()
    {
        if (playerAwarness.AwareOfPlayer)
            targerDirection = playerAwarness.directionToPlayer;
        else
            targerDirection = Vector2.zero;
    }

    private void SetVelocity()
    {
        if (targerDirection == Vector2.zero)
            rb.velocity = Vector2.zero;
        else
            rb.velocity = new Vector2(playerAwarness.directionToPlayer.x * speed, playerAwarness.directionToPlayer.y * speed);
    }
}
