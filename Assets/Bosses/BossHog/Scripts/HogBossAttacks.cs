using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HogBossAttacks : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D rushAoe;

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
}
