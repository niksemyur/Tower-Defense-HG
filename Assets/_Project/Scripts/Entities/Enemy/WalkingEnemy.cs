using UnityEngine;

namespace TowerDefense.Entities.Enemy
{
    public class WalkingEnemy : BaseEnemy
    {
        protected override void MoveToTarget(Vector3 target)
        {
            // Движение
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                MoveSpeed * Time.deltaTime
            );

            // Плавный поворот
            if ((target - transform.position).sqrMagnitude > RotationThreshold)
            {
                Vector3 direction = (target - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    RotationSpeed * Time.deltaTime
                );
            }
        }
    }
}