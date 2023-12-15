using Bryndzaky.Units;
using Bryndzaky.Units.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    public bool spawnerEnabled = true;
    protected Collider2D spawnerCollider;

    [Serializable]
    protected abstract class EntitySpawn : MonoBehaviour
    {
        public GameObject entityPrefab;
        public bool Active { get; protected set; } = true;
        [SerializeField] protected float respawnTime;
        protected float remaining = 0;
        protected GameObject entityObject;
        protected Spawner spawner; 

        public void Start()
        {
            this.spawner = GetComponent<Spawner>();
            this.Respawn();
            this.remaining = this.respawnTime / Time.fixedDeltaTime;
        }

        public void Update()
        {
            if (this.spawner.enabled)
                this.Active = this.entityObject != null;
        }
        
        public void FixedUpdate()
        {
            if (this.spawner.enabled)
            {
                if (this.Active)
                    return;

                if (--remaining <= 0)
                {
                    this.Respawn();
                    this.remaining = this.respawnTime / Time.fixedDeltaTime;
                }
            }
        }
        public virtual void Respawn()
        {
            var xSpawn = UnityEngine.Random.Range(this.spawner.spawnerCollider.bounds.min.x, this.spawner.spawnerCollider.bounds.max.x);
            var ySpawn = UnityEngine.Random.Range(this.spawner.spawnerCollider.bounds.min.y, this.spawner.spawnerCollider.bounds.max.y);
            this.entityObject = Instantiate(this.entityPrefab, new Vector3(xSpawn, ySpawn, 0), transform.rotation);
        }
    }

    // [SerializeField] private List<Entity> entities; 
}
