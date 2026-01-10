using UnityEngine;
using System.Collections;
using TowerDefense.Configs;

namespace TowerDefense.Gameplay.Managers
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Containers")]
        [SerializeField] private Transform enemiesContainer; // Транформ для врагов на сцене

        [Header("Path")]
        [SerializeField] private Transform spawnPoint; 
        [SerializeField] private Transform[] enemyPathPoints; // Поинты пути движения врагов

        [Header("Factory")]
        [SerializeField]  private EnemyFactory _enemyFactory;

        private Coroutine _spawningCoroutine;

        private LevelConfig _levelConfig;

        public void Init (LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        public void Load ()
        {
            StartSpawning();
        }

        public void StartSpawning()
        {
            StopSpawning();
            _spawningCoroutine = StartCoroutine(SpawnEnemiesRoutine());
        }

        public void StopSpawning()
        {
            if (_spawningCoroutine != null)
            {
                StopCoroutine(_spawningCoroutine);
                _spawningCoroutine = null;
            }
        }

        private IEnumerator SpawnEnemiesRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_levelConfig.EnemySpawnTime);

                var enemy = _enemyFactory.CreateEnemy(
                    _levelConfig.EnemyData,
                    spawnPoint,
                    enemyPathPoints,
                    enemiesContainer
                );

                if (enemy == null)
                {
                    Debug.LogWarning("Не удалось создать врага - " + enemy.name + "");
                }
            }
        }

        private void OnDestroy()
        {
            StopSpawning();
        }
    }
}