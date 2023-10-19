using Bryndzaky.Combat;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public abstract class ProjectileAbstract : MonoBehaviour, ICombatCollision
{
    private float timer = 0;
    private int stopProjectile = 2;
    [HideInInspector]
    public int damage = 10;
    [SerializeField]
    private float speed = 10f;
    [HideInInspector]
    public int freezingTime = 10;
    public GameObject hitEffect;
    [HideInInspector]
    public string source;

    protected virtual void Update()
    {
        if (timer < stopProjectile)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
            timer = 0;
        }
    }

    public abstract void OnTriggerEnter2D(Collider2D other);

    protected virtual void Awake()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = speed * transform.right;
    }

    protected virtual void PlayHitEffect()
    {
        if (this.hitEffect == null)
            return;

        GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effect, 0.2f);
        Destroy(gameObject);
    }
}
