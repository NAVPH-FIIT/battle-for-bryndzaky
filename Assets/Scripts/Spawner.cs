using Bryndzaky.Units;
using Bryndzaky.Units.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Serializable]
    protected class Entity : MonoBehaviour
    {
        public GameObject prefab;
        public new bool enabled = true;
        public bool Active { get; protected set; } = true;
        [SerializeField] protected float respawnTime;
        protected float remaining = 0;
        protected GameObject entityObject;
        protected new Collider2D collider;

        public void Start()
        {
            this.Respawn();
            this.remaining = respawnTime;
        }

        public void Update()
        {
            if (this.enabled)
                this.Active = this.entityObject != null;
        }
        
        public void FixedUpdate()
        {
            if (this.enabled)
            {
                if (this.Active)
                    return;

                if (remaining <= 0)
                {
                    this.Respawn();
                    this.remaining = this.respawnTime;
                }
            }
        }
        public virtual void Respawn()
        {
            var xSpawn = UnityEngine.Random.Range(collider.bounds.min.x, collider.bounds.max.x);
            var ySpawn = UnityEngine.Random.Range(collider.bounds.min.y, collider.bounds.max.y);
            this.entityObject = Instantiate(this.prefab, new Vector3(xSpawn, ySpawn, 0), transform.rotation);
        }
    }

    [SerializeField] private List<Entity> entities; 
}
