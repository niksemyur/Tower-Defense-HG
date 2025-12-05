using UnityEngine;
using TowerDefense.Configs;
using TowerDefense.Entities.Enemy;

namespace TowerDefense.Gameplay.Managers
{
    public class EnemyFactory : MonoBehaviour
    {
        private EnemyData _enemyData;

        public void Init (EnemyData enemyData)
        {
            _enemyData = enemyData;
        }

        public BaseEnemy CreateEnemy(string enemyId, Transform spawnPoint, Transform[] enemyPathPoints, Transform enemiesContainer)
        {
            EnemyInfo enemyInfo = _enemyData.GetEnemyInfo(enemyId);
            GameObject enemyObject = Instantiate(enemyInfo.EnemyPrefab, spawnPoint.position, spawnPoint.rotation, enemiesContainer);
            BaseEnemy enemy = enemyObject.GetComponent<BaseEnemy>();

            if (enemy == null)
            {
                Debug.LogError($"Префаб врага '{enemyId}' не содержит компонент BaseEnemy!");
                Destroy(enemyObject);
                return null;
            }

            enemy.Init(enemyInfo.Health, enemyInfo.Reward);
            enemy.SetPath(enemyPathPoints);
            return enemy;
        }
    }
}