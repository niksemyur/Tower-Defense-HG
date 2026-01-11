using UnityEngine;
using TowerDefense.Configs;
using TowerDefense.Entities.Enemy;
using Zenject;

namespace TowerDefense.Gameplay.Managers
{
    public class EnemyFactory
    {
        private readonly DiContainer _container;

        [Inject]
        public EnemyFactory(DiContainer container)
        {
            _container = container;
        }

        public BaseEnemy CreateEnemy(EnemyData enemyData, Transform spawnPoint, Transform[] enemyPathPoints, Transform enemiesContainer)
        {
            if (enemyData.EnemyPrefab == null)
            {
                Debug.LogError($"Префаб врага '{enemyData.name}' не назначен!");
                return null;
            }

            GameObject enemyObject = _container.InstantiatePrefab(
                enemyData.EnemyPrefab,
                spawnPoint.position,
                spawnPoint.rotation,
                enemiesContainer
            );

            BaseEnemy enemy = enemyObject.GetComponent<BaseEnemy>();
            if (enemy == null)
            {
                Debug.LogError($"Префаб врага '{enemyData.name}' не содержит компонент BaseEnemy!");
                Object.Destroy(enemyObject);
                return null;
            }

            enemy.Initialize(enemyPathPoints);
            return enemy;
        }
    }
}