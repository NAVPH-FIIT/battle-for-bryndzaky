using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D weapon1At1;
    [SerializeField]
    private PolygonCollider2D weapon1At2;
    [SerializeField]
    private CircleCollider2D rushAtck;
    [SerializeField]
    private Rigidbody2D boss;
    [SerializeField] 
    private Rigidbody2D player;
    [SerializeField] 
    private Transform weaponAim;
    [SerializeField] 
    private GameObject groundExplosion;

    private Quaternion defRotation;
    
    // Start is called before the first frame update
    public void Attack1_enable()
    {
        weapon1At1.enabled = true;
    }
    public void Attack1_disable()
    {
        weapon1At1.enabled = false;
    }
    public void Attack2_enable()
    {
        weapon1At2.enabled = true;
        groundExplosion.SetActive(true);
    }
    public void Attack2_disable() {
        weapon1At2.enabled=false;
        weaponAim.transform.rotation = defRotation;
        groundExplosion.SetActive(false);
        Vector2 scale = weaponAim.transform.localScale;
        scale.y = 1;
        weaponAim.transform.localScale = scale;
    }
    public void Attack2_aim() {
        defRotation = weaponAim.transform.rotation;
        Vector2 direction = player.position - boss.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Calculate the rotation in degrees
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Vector2 scale = weaponAim.transform.localScale;
        if (direction.x < 0)
            scale.y = -1;
        else
            scale.y = 1;
        weaponAim.transform.localScale = scale;
        weaponAim.transform.rotation = rotation;
    }

    public void Rush_enable()
    {
        rushAtck.enabled = true;
        // enable dust
    }

    public void Rush_disable()
    {
        rushAtck.enabled = false;
        // disable dust
    }

}
