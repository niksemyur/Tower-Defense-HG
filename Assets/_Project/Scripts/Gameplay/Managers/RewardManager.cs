using UnityEngine;
using TowerDefense.Entities.Enemy;

namespace TowerDefense.Gameplay.Managers
{
    // Выдаёт награды за убийство врагов
    public class RewardManager : MonoBehaviour
    {
        private void Awake()
        {
            BaseEnemy.OnEnemyDied += GiveReward;
        }

        private void OnDestroy()
        {
            BaseEnemy.OnEnemyDied -= GiveReward;
        }

        // Выдать награду за убитого врага
        private void GiveReward(BaseEnemy deadEnemy, int rewardAmount)
        {
            CurrencyManager.Instance.AddCurrency(rewardAmount);
        }
    }
}