using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivDeactiv : MonoBehaviour
{
    public GameObject[] conditionalObjects;
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (conditionalObjects.Length > 0)
        {
            foreach (var obj in conditionalObjects)
            {
                // Toggle the active state of each object in the list
                if (obj != null)
                {
                    obj.SetActive(!obj.activeSelf);
                }
            }
        }
    }
}