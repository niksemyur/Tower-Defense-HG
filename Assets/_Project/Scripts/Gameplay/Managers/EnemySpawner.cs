using UnityEngine;
using System.Collections;
using TowerDefense.Configs;
using Zenject;

namespace TowerDefense.Gameplay.Managers
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Containers")]
        [SerializeField] private Transform _enemiesContainer; // Транформ для врагов на сцене

        [Header("Path")]
        [SerializeField] private Transform _spawnPoint; 
        [SerializeField] private Transform[] _enemyPathPoints; // Поинты пути движения врагов

        [Header("Factory")]
        [SerializeField]  private EnemyFactory _enemyFactory;

        private Coroutine _spawningCoroutine;

        private LevelConfig _levelConfig;

        [Inject]
        public void Construct(LevelConfig levelConfig, EnemyFactory enemyFactory)
        {
            _levelConfig = levelConfig;
            _enemyFactory = enemyFactory;
        }

        public void Initialize()
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
                    _spawnPoint,
                    _enemyPathPoints,
                    _enemiesContainer
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