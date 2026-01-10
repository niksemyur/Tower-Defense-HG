using UnityEngine;
using TowerDefense.Entities.Enemy;
using TowerDefense.Entities.Projectiles;
using TowerDefense.Configs;

namespace TowerDefense.Entities.Towers
{
    public class BaseTower : MonoBehaviour //можно сделать абстрактным в последствии
    {
        [Header("Config")]
        [SerializeField] private TowerData towerData;

        [Header("Aiming")]
        [SerializeField] private Transform projectileSpawnPoint; // Транформ появления снаряда
        [SerializeField] private float rotationSpeed = 5f; // Скорость поворота башни (градусы в секунду)
        [SerializeField] private float targetSearchInterval = 0.3f; // Интервал поиска новых целей (секунды)
        [SerializeField] private float aimThreshold = 10f; // Допустимая погрешность прицеливания (градусы)
        [SerializeField] private LayerMask enemyLayer; // Слой для поиска врагов

        [Header("Debug")]
        private bool drawGizmos; // Отрисовка гизмоса

        protected BaseEnemy _currentTarget; // Текущий таргет
        private float _attackTimer; // Текущий таймер перезарядки
        private float _targetSearchTimer; // Текущий интервал поиска новой цели
        private bool _isAimedAtTarget = false; // Проверка наведения на цель

        void Update()
        {
            _attackTimer -= Time.deltaTime;
            _targetSearchTimer -= Time.deltaTime;

            // Если нет цели - ищем
            if (_currentTarget == null)
            {
                if (_targetSearchTimer <= 0)
                {
                    UpdateTarget();
                    _targetSearchTimer = targetSearchInterval;
                }
            }
            else // Если есть цель
            {
                // Проверяем жива ли цель и в радиусе
                if (!_currentTarget.IsAlive || !IsTargetInRange(_currentTarget))
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
                    _attackTimer = towerData.AttackCooldown;
                }
            }
        }

        // Поиск и обновление цели
        private void UpdateTarget()
        {
            if (_currentTarget != null && _currentTarget.IsAlive && IsTargetInRange(_currentTarget))
                return;

            _currentTarget = FindBestTarget();
            _isAimedAtTarget = false;
        }

        // Получение ближайшей цели
        private BaseEnemy FindBestTarget()
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, towerData.Range, enemyLayer);

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
            return Vector3.Distance(transform.position, target.transform.position) <= towerData.Range;
        }

        // Плавный поворот к цели
        private bool RotateTowardsTarget()
        {
            Vector3 direction = _currentTarget.transform.position - transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );

                float angle = Quaternion.Angle(transform.rotation, targetRotation);
                return angle <= aimThreshold;
            }

            return false;
        }

        // Потеря цели
        private void LoseTarget()
        {
            _currentTarget = null;
            _isAimedAtTarget = false;
            _targetSearchTimer = 0;
        }

        private void Attack()
        {
            BaseProjectile newProjectile = Instantiate(towerData.Projectile, projectileSpawnPoint.position, Quaternion.identity);
            newProjectile.Init(towerData.Damage, towerData.ProjectileSpeed, towerData.ProjectileRange);
            newProjectile.Launch(_currentTarget.transform);
        }

        private void OnDrawGizmosSelected()
        {
            if (!drawGizmos) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, towerData.Range);
        }
    }
}