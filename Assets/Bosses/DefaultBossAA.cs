using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBossAA : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int damage = 0;
    public void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag.Equals("Player"))
        {
            collider.GetComponent<Bryndzaky.Units.Unit>().Hit(damage, gameObject.transform.position);
        }
    }

    public void SetDamage(int value)
    {
        damage = value;
    }
}
