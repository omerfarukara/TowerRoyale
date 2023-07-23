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
    public class Player : SpawnObject,IAttackable
    {
        [SerializeField] private CharacterData _characterData;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Transform target;
        [SerializeField] private LayerMask enemyLayer;
        
        Dictionary<Transform, float> spawners = new Dictionary<Transform, float>();

        private Action<SpawnObject> _onComplete;
        private EventData EventData => DataManager.Instance.EventData;

        private Collider[] _enemyColliders;

        private int _mana;
        private Target _targetType;
        public float _attackSpeed;
        public float _sightRange;
        public float _movementSpeed;
        public int _unitAmount;
        public int _damage;
        public int _hitPoints;

        private bool _isAttack;
        public bool IsPlayer { get; set; }

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

        private void OnCollisionEnter(Collision collision)
        {

        }

        // private void FixedUpdate()
        // {
        //     _enemyColliders = Physics.OverlapSphere(transform.position, 5, enemyLayer);
        //
        //     if (_enemyColliders.Length >0)
        //     {
        //         foreach (var collision in _enemyColliders)
        //         {
        //             if (collision.gameObject.TryGetComponent(out IAttacker iAttacker))
        //             {
        //                 if (!iAttacker.IsPlayer)
        //                 {
        //                     iAttacker.Attack();
        //                 }
        //             }
        //         }
        //     }
        // }

        private void Update()
        {
            if (target == null || _isAttack) return;
            agent.SetDestination(target.position);
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }
    }
}