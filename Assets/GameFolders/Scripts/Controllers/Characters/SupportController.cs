using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameFolders.Scripts.Controllers;
using GameFolders.Scripts.General;
using UnityEngine;

namespace TowerRoyale
{
    public class SupportController : SpawnObject,IAttackable
    {
        [SerializeField] private Transform target;
        
        Dictionary<Transform, float> spawners = new Dictionary<Transform, float>();

        private Action<SpawnObject> _onComplete;
        private EventData EventData => DataManager.Instance.EventData;

        private Collider[] _enemyColliders;

        public override void Initialize(Action<SpawnObject> onComplete)
        {
            _onComplete = onComplete;
            
            
            if (GameController.Instance._spawnedAi.Count > 0)
            {
                FindAi();
            }
            else
            {
                FindAiTower();
            }

            target = spawners.OrderBy(s=>s.Value).First().Key.transform;
        }
        
        private void FindAi()
        {
            spawners.Clear();
            foreach (var ai in GameController.Instance._spawnedAi)
            {
                spawners.Add(ai.transform, Vector3.Distance(transform.position, ai.transform.position));
            }
        }
        
        private void FindAiTower()
        {
            spawners.Clear();
            foreach (var tower in GameController.Instance.aiTowers)
            {
                spawners.Add(tower.transform, Vector3.Distance(transform.position, tower.position));
            }
        }
        
        public void Attack()
        {
            target = null;
        }
    }
}
