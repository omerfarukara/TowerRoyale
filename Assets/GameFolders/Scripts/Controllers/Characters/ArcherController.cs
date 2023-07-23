using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameFolders.Scripts.Controllers;
using GameFolders.Scripts.General;
using GameFolders.Scripts.General.Enum;
using GameFolders.Scripts.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace TowerRoyale
{
    public class ArcherController : SpawnObject, IAttackable, IDamageable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private CharacterData _characterData;

        [ShowInInspector] Dictionary<Transform, float> spawners = new Dictionary<Transform, float>();

        private Action<SpawnObject> _onComplete;
        private Collider[] _sightColliders;

        private float _health;

        public float Health
        {
            get => _health;
            set
            {
                _health = value;
                if (Health < 0)
                {
                    Health = 0;
                    Destroy(gameObject);
                }
            }
        }

        private int _mana;
        private Target _targetType;
        public float _attackSpeed;
        public float _attackRange;
        public float _sightRange;
        public float _movementSpeed;
        public int _unitAmount;
        public int _damage;

        private bool _isAttack;
        public Transform target;

        private void Awake()
        {
            VariablesSetLocal();
            SetUnitData();
        }

        private void SetUnitData()
        {
            agent.speed = _movementSpeed;
        }

        private void VariablesSetLocal()
        {
            _mana = _characterData.mana;
            _targetType = _characterData.target;
            _attackSpeed = _characterData.attackSpeed;
            _attackRange = _characterData.attackRange;
            _sightRange = _characterData.sightRange;
            _movementSpeed = _characterData.movementSpeed;
            _unitAmount = _characterData.unitAmount;
            _damage = _characterData.damage;
            Health = _characterData.hitPoints;
        }

        public override void Initialize(Action<SpawnObject> onComplete)
        {
            _onComplete = onComplete;

            switch (gamer)
            {
                case Gamer.Player:
                    FindAiTower();
                    break;
                case Gamer.AI:
                    FindPlayerTower();
                    break;
            }


            target = spawners.OrderBy(s => s.Value).First().Key.transform;
            agent.SetDestination(target.position);
        }


        private void FixedUpdate()
        {
            if (_isAttack) return;

            _sightColliders = Physics.OverlapSphere(transform.position, _sightRange, enemyLayer);
            if (_sightColliders.Length > 0)
            {
                List<Transform> _tranforms = new List<Transform>();
                foreach (var _sight in _sightColliders)
                {
                    switch (gamer)
                    {
                        case Gamer.Player:
                            if (_sight.TryGetComponent(out SpawnObject ai) && ai.gamer == Gamer.AI)
                            {
                                _tranforms.Add(_sight.transform);
                            }

                            break;
                        case Gamer.AI:
                            if (_sight.TryGetComponent(out SpawnObject player) && player.gamer == Gamer.Player)
                            {
                                _tranforms.Add(_sight.transform);
                            }

                            break;
                    }

                    if (_tranforms.Count > 0)
                    {
                        target = FindClosestEnemy(_tranforms);
                        agent.SetDestination(target.position);
                    }
                }
            }
        }

        private void Update()
        {
            if (_isAttack) return;

            if (target != null)
            {
                if (Math.Abs(Vector3.Distance(transform.position, target.position)) > _attackRange)
                {
                    _animator.SetFloat("Force", agent.velocity.magnitude);
                    if ( agent.speed != 0)return;
                    agent.speed = _movementSpeed;
                }
                else
                {
                    Attack();
                }
            }
            else
            {
                switch (gamer)
                {
                    case Gamer.Player:
                        FindAiTower();
                        break;
                    case Gamer.AI:
                        FindPlayerTower();
                        break;
                }
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

        private void FindPlayerTower()
        {
            spawners.Clear();
            foreach (var tower in GameController.Instance.playerTowers)
            {
                spawners.Add(tower.transform, Vector3.Distance(transform.position, tower.position));
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, _sightRange);
        }

        public void Attack()
        {
            IDamageable iDamageable = target.GetComponent<IDamageable>();
            iDamageable.TakeDamage(_damage);

            _isAttack = true;
            
            agent.speed = 0;
            
            _animator.SetTrigger("Attack");

            if (iDamageable.Health> 0)
            {
                Invoke(nameof(Attack), 0.5f);
            }
        }

        private Transform FindClosestEnemy(List<Transform> tranforms)
        {
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            Transform closest = null;
            foreach (Transform go in tranforms)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }

            return closest;
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
        }
    }
}