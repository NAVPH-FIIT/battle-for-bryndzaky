using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RushAttack : StateMachineBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer dustGround;
    private CircleCollider2D rushAoe;
    private Vector2 playerPos;
    private float timeEnd;
    [SerializeField]
    private float rushspeed = 1f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        // TODO mozno cez tag
        rb.bodyType = RigidbodyType2D.Dynamic;
        timeEnd = Time.time + 2f;
        dustGround = animator.GetComponentInChildren<Transform>().Find("MeleeWeapon").Find("Dust1").GetComponent<SpriteRenderer>();
        dustGround.enabled = true;
        rushAoe = animator.GetComponentInChildren<Transform>().Find("MeleeWeapon").Find("RushAttack").GetComponent<CircleCollider2D>();
        playerPos = new Vector2(player.position.x, player.position.y);
        Vector2 direction = (playerPos - rb.position).normalized;
        playerPos += direction;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Vector2 direction = (playerPos - rb.position).normalized;
        rb.velocity = direction * 3f * rushspeed;
        if (Vector2.Distance(playerPos, rb.position) < 0.5 || Time.time > timeEnd) 
        {
            animator.SetBool("RushAttack", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rushAoe.enabled = false;
        dustGround.enabled = false;
    }
}
