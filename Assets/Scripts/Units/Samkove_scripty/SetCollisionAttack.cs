using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCollisionAttack : MonoBehaviour
{
    public int damage = 3;
    public EnemyCollisionAttack attackArea;
    public PlayerAwerness playerAwarness;
    //public float cooldown = 0;
    public float attackCooldown = 1f;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
    private bool attacking = false;


    private bool canAttack = true;
    public CoolDownHandler cdh;
    // Start is called before the first frame update
    void Start()
    {
        attackArea.gameObject.SetActive(false);
        attackArea.SetDamage(damage);
    }
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
        //weaponAnim.SetTrigger("Attack");
        attackArea.gameObject.SetActive(attacking);
        yield return new WaitForSeconds(timeToAttack);
        attacking = false;
        attackArea.gameObject.SetActive(attacking);
        attackArea.attacked = false;

    }
}
