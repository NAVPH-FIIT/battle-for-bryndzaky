using System.Collections;
using System.Collections.Generic;
using Bryndzaky.General.Common;
using Unity.VisualScripting;
using UnityEngine;
using Bryndzaky.Combat.Weapons;
using Bryndzaky.Units.Player;
using Pathfinding;

namespace Bryndzaky.Units.Enemies 
{
    [CreateAssetMenu]
    public class EnemyStats: ScriptableObject
    {
        public int reward_xp;
        public int reward_gold;
        public float moveSpeed = 3;
        public int maxHealth = 100;
    }
}