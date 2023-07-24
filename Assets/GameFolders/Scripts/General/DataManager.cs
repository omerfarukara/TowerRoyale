using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.General;
using UnityEngine;

namespace TowerRoyale
{
    public class DataManager : MonoSingleton<DataManager>
    {
        [SerializeField] private EventData eventData;
        [SerializeField] private CharacterData knightData;
        [SerializeField] private CharacterData archerData;
        [SerializeField] private CharacterData dragonData;
        [SerializeField] private CharacterData supportData;

        public EventData EventData => eventData;
        public CharacterData KnightData => knightData;
        public CharacterData ArcherData => archerData;
        public CharacterData DragonData => dragonData;
        public CharacterData SupportData => supportData;

    }
}
