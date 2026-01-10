using System;
using TowerDefense.Configs;
using UnityEngine;

namespace TowerDefense.Entities.Enemy
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField] private EnemyData enemyData;

        [SerializeField] protected float RotationThreshold = 0.01f; // минимальная дистанция для поворота
        [SerializeField] protected float ArrivalDistance = 0.1f; // минимальная дистанция достижения поинта
        [SerializeField] protected float MoveSpeed = 2f; // скорость движения
        [SerializeField] protected float RotationSpeed = 5f; // скорость поворота

        private Transform[] _pathPoints;

        private int _currentPointIndex;
        private bool _isAlive = true;
        private float _health;
        public bool IsAlive => _isAlive;

        // ПРИМЕЧАНИЕ ДЛЯ РЕВЬЮВЕРА:
        // Статическое событие выбрано осознанно для прототипа Tower Defense, так подписан на него только RewardManager.
        public static event Action<BaseEnemy, int> OnEnemyDied;
        // Плюсы: простота, скорость разработки, меньше кода.
        // Минусы: глобальное состояние, сложнее тестировать.
        // 
        // Когда проект вырастет (5+ систем подписанных на смерть врагов):
        // 1. Заменить на EventBus/ScriptableObject Events

        public void Init(Transform[] pathPoints)
        {
            _health = enemyData.Health;
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

        // КАК двигаться к цели (реализует наследник)
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
            OnEnemyDied?.Invoke(this, enemyData.Reward);
            Destroy(gameObject);
        }

        private void ReachBase ()
        {
            Destroy(gameObject);
        }
    }
}