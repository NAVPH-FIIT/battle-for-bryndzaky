using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwerness : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }
    public Vector2 directionToPlayer { get; private set; }
    public Vector2 enemyToPlayerVector { get; private set; }

    [SerializeField]
    private float playerAwarenessDistance;

    private Transform player;
    //[SerializeField]
    //private new Collider2D collider;

    private void Awake()
    {
        player = FindObjectOfType<MoveScript>().transform;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    Vector2 enemyToPlayerVector = player.position - transform.position;
    //    directionToPlayer = enemyToPlayerVector.normalized;

    //    if (enemyToPlayerVector.magnitude <= playerAwarenessDistance)
    //    {
    //        AwareOfPlayer = true;
    //    }
    //    else
    //    {
    //        AwareOfPlayer = false;
    //    }
    //}
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            enemyToPlayerVector = player.position - transform.position;
            directionToPlayer = enemyToPlayerVector.normalized;
            AwareOfPlayer = true;
        }
    }
    public void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            enemyToPlayerVector = player.position - transform.position;
            directionToPlayer = enemyToPlayerVector.normalized;
            AwareOfPlayer = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        AwareOfPlayer = false;
    }
}
