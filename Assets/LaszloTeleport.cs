using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bryndzaky.Units.Player;

public class LaszloTeleport : MonoBehaviour
{
    public void OnTriggerStay2D(Collider2D collider)
    {
        Player.Instance.gameObject.transform.position = new Vector2(17.1f, 9.9f);
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        Player.Instance.gameObject.transform.position = new Vector2(17.1f, 9.9f);
    }
}
