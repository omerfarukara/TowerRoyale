using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameFolders.Scripts.Controllers;
using GameFolders.Scripts.General;
using GameFolders.Scripts.General.Enum;
using UnityEngine;
using UnityEngine.AI;

namespace TowerRoyale
{
    public class AI : SpawnObject,IAttackable
    {
        [SerializeField] private CharacterData _characterData;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Transform target;
        
        private Action<SpawnObject> _onComplete;
        private EventData EventData => DataManager.Instance.EventData;

        private int _mana;
        private Target _targetType;
        public float _attackSpeed;
        public float _sightRange;
        public float _movementSpeed;
        public int _unitAmount;
        public int _damage;
        public int _hitPoints;
        
        private void Awake()
        {
            _mana = _characterData.mana;
            _targetType = _characterData.target;
            _attackSpeed = _characterData.attackSpeed;
            _sightRange = _characterData.sightRange;
            _movementSpeed = _characterData.movementSpeed;
            _unitAmount = _characterData.unitAmount;
            _damage = _characterData.damage;
            _hitPoints = _characterData.hitPoints;
        }

        public override void Initialize(Action<SpawnObject> onComplete)
        {
            _onComplete = onComplete;
            
            GameController.Instance._spawnedAi.Add(this);
            transform.localRotation = Quaternion.Euler(Vector3.up * 180);

            Dictionary<Transform, float> spawners = new Dictionary<Transform, float>();
            foreach (var tower in GameController.Instance.playerTowers)
            {
                spawners.Add(tower,Vector3.Distance(transform.position,tower.transform.position));
            }
            target = spawners.OrderBy(s=>s.Value).First().Key.transform;
        }

        private void Update()
        {
            if (target == null) return;
            agent.SetDestination(target.position);
        }

        public void Attack()
        {
        }
    }
}