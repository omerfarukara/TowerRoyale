using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameFolders.Scripts.Controllers;
using GameFolders.Scripts.General.Enum;
using GameFolders.Scripts.Interfaces;
using TowerRoyale.GameFolders.Sprites;
using UnityEngine;

namespace TowerRoyale
{
    public class TowerController : MonoBehaviour, IDamageable
    {
        [SerializeField] TowerData towerData;
        [SerializeField] private Animator animator;
        [SerializeField] private HpBar hpBar;
        [SerializeField] private float scanTime;
        [SerializeField] private GameObject visualBody;
        [SerializeField] private ParticleSystem bomb;

        private Collider[] _sightColliders;
        private static readonly int AttackHash = Animator.StringToHash("Attack");

        private float _hitSpeed;
        private float _damage;
        private float _range;
        private float _damagePerSecond;
        private Target _targetType;

        private float _currentScanTime;
        private bool _isAttack;
        private bool _canScan;
        private Transform _target;

        public OwnerType OwnerType { get; }


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


        private void Awake()
        {
            _currentScanTime = scanTime;
            VariablesSetLocal();
            SetUnitData();
        }

        private void VariablesSetLocal()
        {
            Health = towerData.health;
            _hitSpeed = towerData.hitSpeed;
            _damage = towerData.damage;
            _range = towerData.range;
            _damagePerSecond = towerData.damagePerSecond;
            _targetType = towerData.target;
        }

        private void SetUnitData()
        {
            hpBar.Health = Health;
            hpBar.MaxHealth = Health;
        }

        private void FixedUpdate()
        {
            if (_canScan)
            {
                _canScan = false;
                CheckAndSetTarget();
            }
        }

        private void Update()
        {
            _currentScanTime -= Time.deltaTime;
            if (_isAttack) return;

            if (_currentScanTime < 0)
            {
                _isAttack = false;
                _canScan = true;
                _currentScanTime = scanTime;
            }
        }

        private void CheckAndSetTarget()
        {

            _sightColliders = Physics.OverlapSphere(transform.position, _range);

            if (_sightColliders.Length <= 0) return;

            List<Transform> enemyTransforms = new List<Transform>();

            foreach (Collider sight in _sightColliders)
            {
                if (!sight.TryGetComponent(out IDamageable iDamageable)) continue;

                if (iDamageable.OwnerType == OwnerType) continue; // This collider object is mine

                // This collider object is Enemy!
                enemyTransforms.Add(sight.transform);
            }

            Transform newTransform = enemyTransforms.OrderBy(t => Vector3.Distance(t.position, transform.position))
                .FirstOrDefault();
            if (newTransform != null)
            {
                _target = newTransform;
                visualBody.transform.LookAt(_target);
                Attack();
            }
            else
            {
                visualBody.transform.localRotation = Quaternion.identity;
            }
        }

        private void Attack()
        {
            if (!_target.TryGetComponent(out IDamageable iDamageable)) return;
            iDamageable.TakeDamage(_damage);

            animator.SetTrigger(AttackHash);
            _isAttack = true;


            if (iDamageable.Health > 0)
            {
                if (Health > 0)
                {
                    Invoke(nameof(Attack), _hitSpeed);
                }
            }
            else
            {
                _isAttack = false;
            }
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

        private void OnDead()
        {
            //Kule Yıkıldı
            bomb.Play();
            GameController.Instance.RemoveTower(transform);
            gameObject.SetActive(false);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _range);
        }
#endif
    }
}