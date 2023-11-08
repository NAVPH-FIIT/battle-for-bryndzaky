using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour, IDeathrattle
{
    [SerializeField]
    private DoorUnlockScript door;
    public void GrantReward()
    {
        door.unlocked = true;
        Debug.Log("unlock");
    }
}
