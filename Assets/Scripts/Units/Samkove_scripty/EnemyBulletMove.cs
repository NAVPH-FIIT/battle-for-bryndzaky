using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyBulletMove : MonoBehaviour
{
    private float timer = 0;
    private int stopBullet = 2;
    private int damage = 0;
    public GameObject hitEffect;
    // Update is called once per frame
    void Update()
    {
        if (timer < stopBullet)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
            timer = 0;
        }
    }
    public void SetDamage(int value)
    {
        damage = value;
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Shots fired");
        if (collider.tag.Equals("Player"))
        {
            Health health = collider.GetComponent<Health>();
            health.Damage(damage);
            health.InvokeKnockBack(gameObject.transform.position);

            GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(effect, 0.2f);
            Destroy(gameObject);

        }

        if (collider.tag.Equals("Wall") || collider.tag.StartsWith("Enemy_"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(effect, 0.2f);
            Destroy(gameObject);
        }
    }
}
