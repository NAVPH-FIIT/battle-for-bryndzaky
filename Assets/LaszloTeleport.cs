using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bryndzaky.Units.Player;

public class LaszloTeleport : MonoBehaviour
{
    [SerializeField] private GameObject spawnpoint;
    public void OnTriggerStay2D(Collider2D collider)
    {
        Player.Instance.gameObject.transform.position = spawnpoint.transform.position;
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        Player.Instance.gameObject.transform.position = spawnpoint.transform.position;
    }
}
