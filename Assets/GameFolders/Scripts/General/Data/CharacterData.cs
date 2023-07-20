using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerRoyale
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Character")]
    public class CharacterData : ScriptableObject
    {
        public string nickname;
        public Sprite characterSprite;
    }
}
