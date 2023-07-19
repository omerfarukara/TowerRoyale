using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerRoyale
{
    public abstract class SpawnObject : MonoBehaviour
    {
        public abstract void Initialize(Action<SpawnObject> onComplete);
    }
}
