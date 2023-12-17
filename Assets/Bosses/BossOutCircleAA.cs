using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOutCircleAA : MonoBehaviour
{
    [SerializeField]
    private int damage = 0;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player") && Vector2.Distance(collider.transform.position, transform.position) > 4f)
        {
            collider.GetComponent<Bryndzaky.Units.Unit>().Hit(damage, gameObject.transform.position);
        }
    }

    public void SetDamage(int value)
    {
        damage = value;
    }
}
