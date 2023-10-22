using Bryndzaky.Units;
using Bryndzaky.Units.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyObject;
    [SerializeField]
    private GameObject weaponObject;

    private int numberOfEnemies = 0;
    [SerializeField]
    private int maxNumberOfEnemies = 3;
    private List<GameObject> clones; 

    private new Collider2D collider;

    private float xSpawn;
    private float ySpawn;
    private GameObject enemy;
    private GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        clones = new List<GameObject>();
        collider = GetComponent<Collider2D>();
        for (int i=0; i< maxNumberOfEnemies;i++)
        {
            SpawnEnemy();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (clones.Exists(item => item ==null))
        {
            clones.RemoveAll(item => item == null);
            numberOfEnemies--;
        }
        
        if (numberOfEnemies == 0)
        {
            for (int i = 0; i < maxNumberOfEnemies; i++)
            {
                SpawnEnemy();  
            }
        }
    }

    private void SpawnEnemy()
    {
        xSpawn = UnityEngine.Random.Range(collider.bounds.min.x, collider.bounds.max.x);
        ySpawn = UnityEngine.Random.Range(collider.bounds.min.y, collider.bounds.max.y);
        enemy = Instantiate(enemyObject, new Vector3(xSpawn, ySpawn, 0), transform.rotation);
        // weapon = Instantiate(weaponObject, new Vector3(xSpawn, ySpawn, 0), transform.rotation);
        // weapon.transform.localScale = enemy.transform.localScale;
        // weapon.transform.SetParent(enemy.transform);
        // Enemy enemyScript = enemy.GetComponent<Enemy>();
        // enemyScript.GrantWeapon(weapon);
        enemy.GetComponent<Enemy>().GrantWeapon(weapon);
        numberOfEnemies++;
        clones.Add(enemy);
    }

}
