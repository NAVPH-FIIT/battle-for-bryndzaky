using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HogDeath : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject deathEnable;

    private void OnDestroy()
    {
        deathEnable.SetActive(true);
    }
}
