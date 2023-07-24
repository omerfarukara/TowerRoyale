using System;
using GameFolders.Scripts.General.Enum;
using TowerRoyale;
using UnityEngine;

namespace GameFolders.Scripts.General
{
    [CreateAssetMenu(fileName = "EventData", menuName = "Data/Event Data")]
    public class EventData : ScriptableObject
    {
        public Action OnPlay { get; set; }
        public Action<bool> OnFinishLevel { get; set; }
        
        public Action<CharacterType,Vector3> OnSpawnCharacter { get; set; }
        public Action<Vector3> OnRayCharacter { get; set; }
        
        public Action<CharacterType> CharacterValidate { get; set; }
    }
}