using UnityEngine;
using TowerDefense.Configs;
using TowerDefense.Entities.Enemy;

namespace TowerDefense.Gameplay.Managers
{
    public class EnemyFactory : MonoBehaviour
    {
        public BaseEnemy CreateEnemy(EnemyData enemyData, Transform spawnPoint, Transform[] enemyPathPoints, Transform enemiesContainer)
        {
            if (enemyData.EnemyPrefab == null)
            {
                Debug.LogError($"Префаб врага '{enemyData.name}' не назначен!");
                return null;
            }
            GameObject enemyObject = Instantiate(enemyData.EnemyPrefab, spawnPoint.position, spawnPoint.rotation, enemiesContainer);

            BaseEnemy enemy = enemyObject.GetComponent<BaseEnemy>();
            if (enemy == null)
            {
                Debug.LogError($"Префаб врага '{enemyData.name}' не содержит компонент BaseEnemy!");
                Destroy(enemyObject);
                return null;
            }

            enemy.Init(enemyPathPoints);
            return enemy;
        }
    }
}