using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HogBossAttacks : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D rushAoe;
    [SerializeField]
    private PolygonCollider2D spinAoe;

    public void Rush_enable()
    {
        Debug.Log("ena");
        rushAoe.enabled = true;
    }

    public void Rush_disable() 
    {
        Debug.Log("dis");
        rushAoe.enabled = false;
    }

    public void Spin_enable()
    {
        spinAoe.enabled = true;
    }

    public void Spin_disable()
    {
        spinAoe.enabled = false;
    }
}
