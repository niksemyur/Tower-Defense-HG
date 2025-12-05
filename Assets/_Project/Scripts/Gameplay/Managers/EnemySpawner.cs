using UnityEngine;
using System.Collections;

namespace TowerDefense.Gameplay.Managers
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Containers")]
        [SerializeField] private Transform enemiesContainer; // Транформ для врагов на сцене

        [Header("Path")]
        [SerializeField] private Transform spawnPoint; 
        [SerializeField] private Transform[] enemyPathPoints; // Поинты пути движения врагов

        [Header("Difficulty")]
        [SerializeField] private float enemySpawnTime = 1f;
        [SerializeField] private string spawnEnemyId = "test_enemy";

        private EnemyFactory _enemyFactory;
        private Coroutine _spawningCoroutine;

        public void Init(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
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
                yield return new WaitForSeconds(enemySpawnTime);

                var enemy = _enemyFactory.CreateEnemy(
                    spawnEnemyId,
                    spawnPoint,
                    enemyPathPoints,
                    enemiesContainer
                );

                if (enemy == null)
                {
                    Debug.LogWarning("Не удалось создать врага!");
                }
            }
        }

        private void OnDestroy()
        {
            StopSpawning();
        }
    }
}