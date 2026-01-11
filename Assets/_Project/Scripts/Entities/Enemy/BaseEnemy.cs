using System;
using TowerDefense.Configs;
using TowerDefense.Signals;
using UnityEngine;
using Zenject;

namespace TowerDefense.Entities.Enemy
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField] private EnemyData _enemyData;

        [SerializeField] protected float RotationThreshold = 0.01f; // минимальна€ дистанци€ дл€ поворота
        [SerializeField] protected float ArrivalDistance = 0.1f; // минимальна€ дистанци€ достижени€ поинта
        [SerializeField] protected float MoveSpeed = 2f; // скорость движени€
        [SerializeField] protected float RotationSpeed = 5f; // скорость поворота

        private Transform[] _pathPoints;

        private int _currentPointIndex;
        private bool _isAlive = true;
        private float _health;
        public bool IsAlive => _isAlive;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize(Transform[] pathPoints)
        {
            _health = _enemyData.Health;
            _pathPoints = pathPoints;
        }

        private void Update()
        {
            if (!_isAlive || _pathPoints == null) return;

            if (_currentPointIndex >= _pathPoints.Length)
            {
                ReachBase();
                return;
            }

            Vector3 target = _pathPoints[_currentPointIndex].position;
            MoveToTarget(target);

            if (Vector3.Distance(transform.position, target) < ArrivalDistance)
            {
                _currentPointIndex++;
            }
        }

        //  ј  двигатьс€ к цели (реализует наследник)
        protected abstract void MoveToTarget(Vector3 target);

        public void TakeDamage(float damage)
        {
            if (!_isAlive) return;

            _health -= damage;

            if (_health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (!_isAlive) return;
            _isAlive = false;

            var signal = new RewardMoneySignal
            {
                RewardAmount = _enemyData.Reward
            };
            _signalBus.Fire(signal);

            Destroy(gameObject);
        }

        private void ReachBase ()
        {
            Destroy(gameObject);
        }
    }
}