// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SlovakSpawnerScript : MonoBehaviour
// {
//     public GameObject slovak;
//     public float spawnRate = 2;
//     private float timer = 0;
//     private float heightOff = 3;
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (timer < spawnRate)
//         {
//             timer = timer + Time.deltaTime;
//         }
//         else
//         {
//             SpawnSlovak();
//             timer = 0;
//         }
//     }

//     void SpawnSlovak() {
//         float lowest = transform.position.y - heightOff;
//         float highest = transform.position.y + heightOff;
//         float leftest = transform.position.x - heightOff;
//         float rightest = transform.position.x + heightOff;

//         Instantiate(slovak, new Vector3(Random.Range(leftest, rightest), Random.Range(lowest, highest),0), transform.rotation);
//     }
// }
