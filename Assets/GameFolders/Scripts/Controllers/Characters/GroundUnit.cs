using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameFolders.Scripts.Controllers;
using GameFolders.Scripts.General;
using GameFolders.Scripts.General.Enum;
using GameFolders.Scripts.Interfaces;
using GameFolders.Scripts.Managers;
using Sirenix.OdinInspector;
using TowerRoyale.GameFolders.Sprites;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace TowerRoyale
{
    public class GroundUnit : SpawnObject, IAttackable, IDamageable
    {
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterData characterData;
        [SerializeField] private HpBar hpBar;
        [SerializeField] private float scanTime;
        [SerializeField] private UnityEvent onInitiate;
        [SerializeField] private UnityEvent onDead;

        [ShowInInspector] private List<Transform> _towers = new List<Transform>();
        [ShowInInspector] private OwnerType _ownerType;

        private Action<SpawnObject> _onComplete;
        private Collider[] _sightColliders;

        private float _currentScanTime;
        private bool _canScan;

        public OwnerType OwnerType
        {
            get => _ownerType;
            set => _ownerType = value;
        }

        private float _health;

        public float Health
        {
            get => _health;
            set
            {
                _health = value;
                hpBar.Health = value;
            }
        }

        private bool _canMove;
        private int _mana;
        private Target _targetType;
        private float _attackSpeed;
        private float _attackRange;
        private float _sightRange;
        private float _movementSpeed;
        private int _unitAmount;
        private int _damage;

        private bool _isAttack;
        private Transform _target;
        private static readonly int IdleHash = Animator.StringToHash("Idle");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int ForceHash = Animator.StringToHash("Force");

        private void SetUnitData()
        {
            agent.speed = _movementSpeed;
            hpBar.Health = Health;
            hpBar.MaxHealth = Health;
        }

        private void VariablesSetLocal()
        {
            _mana = characterData.mana;
            _targetType = characterData.target;
            _attackSpeed = characterData.attackSpeed;
            _attackRange = characterData.attackRange;
            _sightRange = characterData.sightRange;
            _movementSpeed = characterData.movementSpeed;
            _unitAmount = characterData.unitAmount;
            _damage = characterData.damage;
            Health = characterData.hitPoints;
        }

        public override void
            Initialize(Action<SpawnObject> onComplete, OwnerType ownerType,
                Vector3 position) // This method works every time a unit is produced
        {
            VariablesSetLocal();
            SetUnitData();
            _onComplete = onComplete;
            _ownerType = ownerType;
            _currentScanTime = scanTime;
            _canScan = true;
            _canMove = true;
            _isAttack = false;
            _target = null;
            transform.position = position;
            onInitiate?.Invoke();
            FindTower();
        }

        private void FixedUpdate()
        {
            if(!GameManager.Instance.Playability()) return;

            if (!_canMove) return;

            if (_isAttack) return;

            if (_canScan)
            {
                _canScan = false;
                CheckAndSetTarget();
            }
        }

        private void Update()
        {
            if(!GameManager.Instance.Playability()) return;
            
            if (!_canMove) return;

            if (_isAttack) return;

            _currentScanTime -= Time.deltaTime;

            if (_currentScanTime < 0)
            {
                _canScan = true;
                _currentScanTime = scanTime;
            }

            if (_target != null)
            {
                if (Math.Abs(Vector3.Distance(transform.position, _target.position)) > _attackRange)
                {
                    animator.SetFloat(ForceHash, agent.velocity.magnitude);
                    if (agent.speed != 0) return;
                    agent.speed = _movementSpeed;
                }
                else
                {
                    Attack();
                }
            }
            else
            {
                FindTower();
            }
        }

        private void FindTower()
        {
            _towers = GameController.Instance.GetTowers(_ownerType);
            Transform newTransform =
                _towers.OrderBy(t => Vector3.Distance(t.position, transform.position) ).FirstOrDefault();
            if (newTransform != null)
            {
                _target = newTransform;
                agent.SetDestination(_target.position);
                agent.speed = _movementSpeed;
            }
            animator.SetTrigger(IdleHash);
            print("Idle Hash");
        }

        private void CheckAndSetTarget()
        {
            _sightColliders = Physics.OverlapSphere(transform.position, _sightRange);

            if (_sightColliders.Length <= 0) return;

            List<Transform> enemyTransforms = new List<Transform>();

            foreach (Collider sight in _sightColliders)
            {
                if (!sight.TryGetComponent(out IDamageable iDamageable)) continue;

                if (iDamageable.OwnerType == _ownerType) continue; // This collider object is mine

                // This collider object is Enemy!
                enemyTransforms.Add(sight.transform);
            }

            Transform newTransform = enemyTransforms.OrderBy(t => Vector3.Distance(t.position, transform.position))
                .FirstOrDefault();
            if (newTransform != null)
            {
                _target = newTransform;
                agent.SetDestination(_target.position);
                agent.speed = _movementSpeed;
            }
        }

        public void Attack()
        {
            print("Attack Öncesi");

            if (!_target.TryGetComponent(out IDamageable iDamageable)) return;

            iDamageable.TakeDamage(_damage);

            _isAttack = true;

            agent.speed = 0;

            print("Attack Hash");
            animator.SetTrigger(AttackHash);


            if (Health > 0)
            {
                if (iDamageable.Health > 0)
                {
                    Invoke(nameof(Attack), 0.5f);
                }
                else
                {
                    _isAttack = false;
                    FindTower();
                }
            }
        }

        private void OnDead()
        {
            print("Öldü");
            _onComplete?.Invoke(this);
            onDead?.Invoke();
            _canMove = false;
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                OnDead();
            }
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, _sightRange);
        }
#endif
    }
}