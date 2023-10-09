using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackGun : MonoBehaviour
{
    public FireBullet spawner;
    public CoolDownHandler cdh;
    public int damage = 10;
    private bool canAttack = true;
    public float setcooldown = 2;
    public float burstcooldown = 0.1f;
    [SerializeField]
    private bool burst = false;
    [SerializeField]
    private int burstSize = 5;
    private int bulletCount = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && canAttack)
        {
            if (burst)
            {
                if (bulletCount == burstSize)
                {
                    canAttack = false;
                    StartCoroutine(cdh.SimpleCooldown(setcooldown, (bool result) => canAttack = result));
                    bulletCount = 0;
                }
                else
                {
                    Attack();
                    canAttack = false;
                    StartCoroutine(cdh.SimpleCooldown(burstcooldown, (bool result) => canAttack = result));
                    bulletCount++;
                }
            }
            else 
            {
                Attack();
                canAttack = false;
                StartCoroutine(cdh.SimpleCooldown(setcooldown, (bool result) => canAttack = result));
            }
        }



    }
    private void Attack()
    {
        spawner.SetDamage(damage);
        spawner.Fire();
    }


}

