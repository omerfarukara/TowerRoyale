using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.General;
using GameFolders.Scripts.General.Enum;
using UnityEngine;

namespace TowerRoyale
{
    public class Character : SpawnObject
    {
        private Action<SpawnObject> _onComplete;
        private EventData EventData => DataManager.Instance.EventData;

        public override void Initialize(CharacterType type, Action<SpawnObject> onComplete)
        {
            _onComplete = onComplete;
        }
    }
}