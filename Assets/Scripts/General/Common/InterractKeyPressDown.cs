using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressDown : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && !PauseManager.IsPaused)
        {
          animator.SetTrigger("KeyDown");
        }
    }
}
