using UnityEngine;

namespace TowerDefense.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "_Assets/_Project/Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [Header("Difficulty")]
        [SerializeField] private float _enemySpawnTime = 1f;
        [SerializeField] private EnemyData _enemyData;

        public float EnemySpawnTime => _enemySpawnTime;
        public EnemyData EnemyData => _enemyData;
    }
}