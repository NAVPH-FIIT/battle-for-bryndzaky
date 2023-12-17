using Bryndzaky.Units.Player;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Sheep : MonoBehaviour
{
    public bool run = false;
    [SerializeField]
    protected Rigidbody2D rb;
    private int currentWaypoint = 0;
    [SerializeField]
    private GameObject target;
    private Vector2 moveDirection;
    [SerializeField]
    private float moveSpeed = 3;
    [SerializeField]
    private Transform[] waypoints;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = Vector2.zero;
        InvokeRepeating("adjustSpeed",0,0.5f);
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (run)
        {
            getDirection();
            move();
        }
    }

    private void getDirection()
    {
        if (currentWaypoint >= waypoints.Length)
            return;
        moveDirection = ((Vector2)waypoints[currentWaypoint].position - rb.position).normalized;
        if (Vector2.Distance(rb.position, waypoints[currentWaypoint].position) < 0.2f)
        {
            currentWaypoint++;
        }
    }

    private void move()
    {
        if (isTooFar())
        {
            rb.velocity = Vector2.zero;
            return;
        }
        rb.velocity = moveSpeed * moveDirection;
    }

    private bool isTooFar()
    {
        return (Vector2.Distance(rb.position, Player.Instance.gameObject.transform.position) > 7);
    }

    private void adjustSpeed()
    {
        if (isTooFar())
        {
            return;
        }
        if (Vector2.Distance(rb.position, Player.Instance.gameObject.transform.position) > 5)
        {
            // slow down
            moveSpeed -= 1;
        }
        else
        {
            //speed up
            moveSpeed += 1;
        }
    }
}
