using UnityEngine;

namespace TowerDefense.Entities.Projectiles
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        [SerializeField] protected LayerMask EnemyLayer;

        protected float Damage = 10f;
        protected float Speed = 10f;
        protected float Range;
        protected Transform Target;

        private Vector3 _targetPosition;

        public void Init(float projectileDamage, float projectileSpeed, float projectileRange)
        {
            Damage = projectileDamage;
            Speed = projectileSpeed;
            Range = projectileRange;
        }

        public void Launch(Transform targetTransform)
        {
            Target = targetTransform;
        }

        private void Update()
        {
            // Если цель жива - летим к ней
            if (Target != null)
            {
                _targetPosition = Target.position;
            }
            // Если цель умерла - летим к последней известной позиции

            MoveToTarget();

            // Проверка достижения цели
            if (Vector3.Distance(transform.position, _targetPosition) < 0.2f)
            {
                OnReachTarget();
                Destroy(gameObject);
            }
        }

        private void MoveToTarget()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _targetPosition,
                Speed * Time.deltaTime
            );

            transform.LookAt(_targetPosition);
        }

        protected abstract void OnReachTarget();
    }
}