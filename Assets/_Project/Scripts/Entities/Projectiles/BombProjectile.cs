using UnityEngine;
using TowerDefense.Entities.Enemy;

namespace TowerDefense.Entities.Projectiles
{    // Снаряд который взрывается при достижении цели, нанося урон по области
    public class BombProjectile : BaseProjectile
    {
        [SerializeField] private float _range = 3f; // Радиус взрыва
        protected override void OnReachTarget()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _range, EnemyLayer);
            foreach (var hit in hits)
            {
                var enemy = hit.GetComponent<BaseEnemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(Damage);
                }
            }
        }
    }
}
