using UnityEngine;
using TowerDefense.Entities.Enemy;

namespace TowerDefense.Entities.Projectiles
{    // Снаряд который взрывается при достижении цели, нанося урон по области
    public class BombProjectile : BaseProjectile
    {
        protected override void OnReachTarget()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, Range, EnemyLayer);
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
