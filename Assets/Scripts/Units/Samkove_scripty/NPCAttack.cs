using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttack : MonoBehaviour
{
    public int damage = 10;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
    public NPCAttackScrip attack;
    public PlayerAwerness playerAwarness;
    public float cooldown = 0;
    public float attackCooldown = 1;
    public Animator weaponAnim;
    public CoolDownHandler cdh;

    private bool canAttack = true;

    void Start()
    {
        attack.gameObject.SetActive(attacking);
        //playerAwarness = GetComponent<PlayerAwerness>();
        attack.SetDamage(damage);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAwarness.AwareOfPlayer && canAttack)
        {
            StartCoroutine(Attack());
            canAttack = false;
            StartCoroutine(cdh.SimpleCooldown(attackCooldown, (bool result) => canAttack = result));
        }

    }

    private IEnumerator Attack()
    {
        attacking = true;
        weaponAnim.SetTrigger("Attack");
        attack.gameObject.SetActive(attacking);
        yield return new WaitForSeconds(timeToAttack);
        attacking = false;
        attack.gameObject.SetActive(attacking);
        attack.attacked = false;
    }
}
