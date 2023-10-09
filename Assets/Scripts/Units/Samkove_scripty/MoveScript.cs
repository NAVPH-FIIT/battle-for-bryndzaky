using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float moveSpeed = 4;
    public float dashSpeed = 20;
    public float dashDuration = 0.25f;
    public float dashCoolDown = 1f;
    public Rigidbody2D rb;
    public Vector2 moveDirection;
    public Animator animator;
    public Collider2D playerHitBox;
    public CoolDownHandler cdh;

    private float currentSpeed;
    private bool canDash = true;
    private bool isDashing = false;

    private void Start()
    {
        currentSpeed = moveSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
            canDash = false;
            StartCoroutine(cdh.SimpleCooldown(dashCoolDown, (bool result) => canDash = result));
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * currentSpeed, moveDirection.y * currentSpeed);
    }

    private IEnumerator Dash()
    {
        currentSpeed = dashSpeed;
        playerHitBox.enabled = false;
        isDashing = true;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        playerHitBox.enabled = true;
        currentSpeed = moveSpeed;
    }
}
