using UnityEngine;
using TowerDefense.Entities.Enemy;
using TowerDefense.Entities.Projectiles;
using TowerDefense.Configs;

namespace TowerDefense.Entities.Towers
{
    public class BaseTower : MonoBehaviour //можно сделать абстрактным в последствии
    {
        [Header("Config")]
        [SerializeField] private TowerData _towerData;

        [Header("Aiming")]
        [SerializeField] private Transform _projectileSpawnPoint; // Транформ появления снаряда
        [SerializeField] private float _rotationSpeed = 5f; // Скорость поворота башни (градусы в секунду)
        [SerializeField] private float _targetSearchInterval = 0.3f; // Интервал поиска новых целей (секунды)
        [SerializeField] private float _aimThreshold = 10f; // Допустимая погрешность прицеливания (градусы)
        [SerializeField] private LayerMask _enemyLayer; // Слой для поиска врагов

        [Header("Debug")]
        private bool _drawGizmos; // Отрисовка гизмоса

        private float _attackTimer; // Текущий таймер перезарядки
        private float _targetSearchTimer; // Текущий интервал поиска новой цели
        private bool _isAimedAtTarget = false; // Проверка наведения на цель

        protected BaseEnemy СurrentTarget; // Текущий таргет

        void Update()
        {
            _attackTimer -= Time.deltaTime;
            _targetSearchTimer -= Time.deltaTime;

            // Если нет цели - ищем
            if (СurrentTarget == null)
            {
                if (_targetSearchTimer <= 0)
                {
                    UpdateTarget();
                    _targetSearchTimer = _targetSearchInterval;
                }
            }
            else // Если есть цель
            {
                // Проверяем жива ли цель и в радиусе
                if (!СurrentTarget.IsAlive || !IsTargetInRange(СurrentTarget))
                {
                    LoseTarget();
                    return;
                }

                // Поворачиваемся и проверяем наведение
                _isAimedAtTarget = RotateTowardsTarget();

                // Если навели и готовы стрелять - стреляем
                if (_isAimedAtTarget && _attackTimer <= 0)
                {
                    Attack();
                    _attackTimer = _towerData.AttackCooldown;
                }
            }
        }

        // Поиск и обновление цели
        private void UpdateTarget()
        {
            if (СurrentTarget != null && СurrentTarget.IsAlive && IsTargetInRange(СurrentTarget))
                return;

            СurrentTarget = FindBestTarget();
            _isAimedAtTarget = false;
        }

        // Получение ближайшей цели
        private BaseEnemy FindBestTarget()
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, _towerData.Range, _enemyLayer);

            if (enemies.Length == 0) return null;

            BaseEnemy bestTarget = null;
            float closestDistance = float.MaxValue;

            foreach (Collider collider in enemies)
            {
                var enemy = collider.GetComponent<BaseEnemy>();
                if (enemy == null || !enemy.IsAlive) continue;

                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    bestTarget = enemy;
                }
            }

            return bestTarget;
        }


        // Проверка что цель в радиусе
        private bool IsTargetInRange(BaseEnemy target)
        {
            return Vector3.Distance(transform.position, target.transform.position) <= _towerData.Range;
        }

        // Плавный поворот к цели
        private bool RotateTowardsTarget()
        {
            Vector3 direction = СurrentTarget.transform.position - transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    _rotationSpeed * Time.deltaTime
                );

                float angle = Quaternion.Angle(transform.rotation, targetRotation);
                return angle <= _aimThreshold;
            }

            return false;
        }

        // Потеря цели
        private void LoseTarget()
        {
            СurrentTarget = null;
            _isAimedAtTarget = false;
            _targetSearchTimer = 0;
        }

        private void Attack()
        {
            BaseProjectile newProjectile = Instantiate(_towerData.Projectile, _projectileSpawnPoint.position, Quaternion.identity);
            newProjectile.Init(_towerData.Damage, _towerData.ProjectileSpeed, _towerData.ProjectileRange);
            newProjectile.Launch(СurrentTarget.transform);
        }

        private void OnDrawGizmosSelected()
        {
            if (!_drawGizmos) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _towerData.Range);
        }
    }
}