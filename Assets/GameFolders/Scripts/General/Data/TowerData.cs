using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.General.Enum;
using UnityEngine;

namespace TowerRoyale
{
    [CreateAssetMenu(fileName = "TowerData", menuName = "Data /TowerData")]
    public class TowerData : ScriptableObject
    {
        public float health;
        public float hitSpeed;
        public float damage;
        public float range;
        public float damagePerSecond;
        public Target target;
    }
}
