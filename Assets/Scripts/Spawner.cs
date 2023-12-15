using Bryndzaky.Units;
using Bryndzaky.Units.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    protected abstract List<IEntity> Entities {get;}
    public bool spawnerEnabled = true;
    protected Collider2D spawnerCollider;

    protected interface IEntity
    {
        public void ManualStart(Spawner spawner);
        public void ManualUpdate();
        public void ManualFixedUpdate();
        public void Respawn();
    }

    [Serializable]
    public abstract class EntitySpawn: IEntity
    {
        public GameObject entityPrefab;
        public bool Active { get; protected set; } = true;
        [SerializeField] protected float? respawnTime;
        protected float remaining = 0;
        protected GameObject entityObject;
        protected Spawner spawner; 

        public void ManualStart(Spawner spawner)
        {
            this.spawner = spawner;
            this.Respawn();
            if (this.respawnTime != null)
                this.remaining = (float) this.respawnTime / Time.fixedDeltaTime;
        }

        public void ManualUpdate()
        {
            if (this.spawner.enabled)
                this.Active = this.entityObject != null;
        }
        
        public void ManualFixedUpdate()
        {
            if (this.spawner.enabled)
            {
                if (this.Active || this.respawnTime == null)
                    return;

                if (--remaining <= 0)
                {
                    this.Respawn();
                    this.remaining = (float) this.respawnTime / Time.fixedDeltaTime;
                }
            }
        }
        public virtual void Respawn()
        {
            var xSpawn = UnityEngine.Random.Range(this.spawner.spawnerCollider.bounds.min.x, this.spawner.spawnerCollider.bounds.max.x);
            var ySpawn = UnityEngine.Random.Range(this.spawner.spawnerCollider.bounds.min.y, this.spawner.spawnerCollider.bounds.max.y);
            this.entityObject = Instantiate(this.entityPrefab, new Vector3(xSpawn, ySpawn, 0), spawner.transform.rotation);
        }

    }
    public void Start()
    {
        this.spawnerCollider = GetComponent<Collider2D>();
        foreach (IEntity entity in this.Entities)
            entity.ManualStart(this);
    }
    public void Update()
    {
        foreach (IEntity entity in this.Entities)
            entity.ManualUpdate();
    }
    public void FixedUpdate()
    {
        foreach (IEntity entity in this.Entities)
            entity.ManualFixedUpdate();
    }
    // [SerializeField] private List<Entity> entities; 
}
