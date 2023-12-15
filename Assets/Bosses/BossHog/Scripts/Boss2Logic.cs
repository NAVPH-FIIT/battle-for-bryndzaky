using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Playables;

public class Boss2Logic : StateMachineBehaviour
{
    private Seeker seeker;
    private Transform player;
    private Rigidbody2D rb;
    private Pathfinding.Path path;
    private int currentWaypoint = 0;
    private bool reachedEnDOfPath = false;
    private Vector2 direction;
    [SerializeField]
    private float nextWaypointDistance = 3;
    private float nextUpdate = 0f;
    private float nextChoice = 0f;
    [SerializeField]
    private float speed = 1f;
    private int randomChoice = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seeker = animator.GetComponent<Seeker>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        UpdatePath();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time > nextUpdate)
        {
            UpdatePath();
            nextUpdate = Time.time + 0.5f;
        }
        Move();
        if (rb.velocity == Vector2.zero)
        {
            animator.SetFloat("Speed", 0);
        }
        else
        {
            Vector2 playerPos = new Vector2(player.position.x, player.position.y);
            Vector2 direction = (playerPos - rb.position).normalized;
            animator.SetFloat("Speed", 1);
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
        if (Time.time > nextChoice)
        {
            Debug.Log(Vector2.Distance(rb.position, player.position));
            
            randomChoice = Random.Range(0, 10);
            if (Vector2.Distance(rb.position, player.position) > 6f)
            {
                
                Debug.Log(randomChoice);
                if (randomChoice == 0 && animator.GetInteger("RushCounter") == 3)
                    animator.SetInteger("RushCounter", 0);
            }
            else if ((Vector2.Distance(rb.position, player.position) < 4f) && (randomChoice < 6))
            {
                animator.SetTrigger("Spin");
            }
            nextChoice = Time.time + 0.15f;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Spin");
        rb.bodyType = RigidbodyType2D.Static;
    }

    void UpdatePath()
    {
        seeker.StartPath(rb.position, player.position, OnPathComplete);
        currentWaypoint = 0;
    }

    private void OnPathComplete(Pathfinding.Path p)
    {
        if (!p.error)
        {
            path = p;
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


        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 velo = speed * direction * 3; //* Time.deltaTime;

        rb.velocity = velo;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

}
