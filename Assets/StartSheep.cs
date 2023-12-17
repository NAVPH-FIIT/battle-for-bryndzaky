using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StartSheep : MonoBehaviour, IDeathrattle
{
    [SerializeField]
    private Sheep sheepscript;
    [SerializeField]
    private Rigidbody2D sheeprb;
    [SerializeField]
    private DoorUnlockScript door;
    public void GrantReward()
    {
        sheeprb.bodyType = RigidbodyType2D.Dynamic;
        sheepscript.run = true;
        door.unlocked = true;
        Debug.Log("sheep started");
    }
}
