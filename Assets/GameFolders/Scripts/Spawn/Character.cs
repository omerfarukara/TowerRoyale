using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerRoyale
{
    public class Character : SpawnObject
    {
        private Action<SpawnObject> _onComplete;
        
        public override void Initialize(Action<SpawnObject> onComplete)
        {
            _onComplete = onComplete;
        }
    }
}
