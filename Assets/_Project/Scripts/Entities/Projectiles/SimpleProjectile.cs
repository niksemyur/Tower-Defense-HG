using TowerDefense.Entities.Enemy;

namespace TowerDefense.Entities.Projectiles {
    public class SimpleProjectile : BaseProjectile
    {
        // Простой снаряд который наносит урон только прямой цели
        protected override void OnReachTarget()
        {
            if (Target)
            {
                Target.GetComponent<BaseEnemy>().TakeDamage(Damage);
            }
        }
    }
}
