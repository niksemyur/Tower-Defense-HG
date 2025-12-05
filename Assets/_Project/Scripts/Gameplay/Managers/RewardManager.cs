using UnityEngine;
using TowerDefense.Entities.Enemy;

namespace TowerDefense.Gameplay.Managers
{
    // Выдаёт награды за убийство врагов
    public class RewardManager : MonoBehaviour
    {
        public void Init()
        {
            // ПРИМЕЧАНИЕ ДЛЯ РЕВЬЮВЕРА:
            // Статическое событие выбрано осознанно для прототипа Tower Defense.
            // Плюсы: простота, скорость разработки, меньше кода.
            // Минусы: глобальное состояние, сложнее тестировать.
            // 
            // Когда проект вырастет (5+ систем подписанных на смерть врагов):
            // 1. Заменить на EventBus/ScriptableObject Events
            // 2. Добавить параметры в событие (тип врага, способ смерти)
            // 3. Сделать интерфейс IRewardable для тестирования
            //
            BaseEnemy.OnEnemyDied += GiveReward;
        }

        private void OnDestroy()
        {
            // Отписываемся при уничтожении
            BaseEnemy.OnEnemyDied -= GiveReward;
        }

        // Выдать награду за убитого врага
        private void GiveReward(BaseEnemy deadEnemy)
        {
            int money = deadEnemy.RewardAmount;
            CurrencyManager.Instance.AddCurrency(money);
        }
    }
}