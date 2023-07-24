using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.General.Enum;
using UnityEngine;

namespace TowerRoyale
{
    [CreateAssetMenu(fileName = "Character Data", menuName = "Data /Character Data")]
    public class CharacterData : ScriptableObject
    {
        public string nickname;
        public Sprite characterSprite;

        public int mana;
        public float attackSpeed;
        public float attackRange;
        public float sightRange;
        public Target target;
        public float movementSpeed;
        public int unitAmount = 1;
        public int damage;
        public int hitPoints;
    }
}
