using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHog : MonoBehaviour
{
    [SerializeField]
    private GameObject hog;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            try
            {
                hog.SetActive(true);
            }catch (System.Exception e)
            {
                Debug.Log("Hog missing");
            }
        
        Debug.Log("hog spawned");
        }
    }
}
