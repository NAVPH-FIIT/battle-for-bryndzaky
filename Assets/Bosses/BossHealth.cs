using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField]
    private float health = 100;
//Start is called before the first frame update
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(int dealt)
    {
        health -= dealt;
        Debug.Log("Auu");
    }
}
