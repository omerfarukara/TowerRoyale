using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.General.Enum;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerRoyale
{
    public abstract class SpawnObject : MonoBehaviour
    {
        public abstract void Initialize(CharacterType type,Action<SpawnObject> onComplete);
    }
}
