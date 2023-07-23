using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.General.Enum;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace TowerRoyale
{
    public abstract class SpawnObject : MonoBehaviour
    {
        public NavMeshAgent agent;
        public abstract void Initialize(Action<SpawnObject> onComplete, OwnerType ownerType, Vector3 position);
    }
}