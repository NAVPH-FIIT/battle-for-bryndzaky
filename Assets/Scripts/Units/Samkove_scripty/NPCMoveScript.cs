using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCMoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5;
    public Animator animator;
    public Rigidbody2D rb;
    public float DeadZonePlusX = 8.5f;
    public float DeadZoneMinusX = -8.5f;
    public float DeadZoneMinusY = -4.5f;
    public float DeadZonePlusY = 4.5f;
    public float timer = 0;
    Vector2Int newDirect;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > DeadZonePlusX || transform.position.x < DeadZoneMinusX || transform.position.y > DeadZonePlusY || transform.position.y < DeadZoneMinusY)
        {
            rb.velocity = new Vector2Int(0, 0);
        }
        animator.SetFloat("Horizontal", newDirect.x);
        animator.SetFloat("Vertical", newDirect.y);
        animator.SetFloat("Speed", 1);
    }
    private void FixedUpdate()
    {
        if (timer < 1)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            Move();
            timer = 0;
        }

    }

    void Move()
    {
        //transform.position = transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;
        newDirect = Direction2D.GetRandomCardinalDirection();
        rb.velocity = newDirect;
       
    }

    public static class Direction2D
    {
        public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int> {

    new Vector2Int(0, 2),  //UP
    new Vector2Int(1, 2),  //UP Right
    new Vector2Int(-1, 2),  //UP Left

    new Vector2Int(2, 0),  //RIGHT
    new Vector2Int(-2, 0), //LEFT

    new Vector2Int(0, -2), //DOWN
    new Vector2Int(1, -2), //DOWN Right
    new Vector2Int(-1, -2), //DOWN left
      
    };

        public static Vector2Int GetRandomCardinalDirection()
        {
            return cardinalDirectionList[UnityEngine.Random.Range(0, cardinalDirectionList.Count)];
        }

    }
}
