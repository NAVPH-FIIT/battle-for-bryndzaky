using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StartSheep : MonoBehaviour, IDeathrattle
{
    [SerializeField]
    private Sheep sheep;
    [SerializeField]
    private DoorUnlockScript door;
    public void GrantReward()
    {
        sheep.run = true;
        door.unlocked = true;
        Debug.Log("sheep started");
    }
}
