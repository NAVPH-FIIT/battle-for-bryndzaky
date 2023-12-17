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
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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

        if (rb.velocity != Vector2.zero)
        {
            animator.SetFloat("Horizontal", moveDirection.x);
            animator.SetFloat("Vertical", moveDirection.y);
            //animator.SetFloat("Speed", new Vector2(playerDirection.x, playerDirection.y).sqrMagnitude);
            animator.SetBool("Speed", true);
        }
        else
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", -1);
            animator.SetBool("Speed", false);
        }
    }

    private bool isTooFar()
    {
        return (Vector2.Distance(rb.position, Player.Instance.gameObject.transform.position) > 10);
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
            if (moveSpeed>=2)
                moveSpeed -= 1;
        }
        else
        {
            //speed up
            if (moveSpeed<=12)
            moveSpeed += 1;
        }
    }
}
