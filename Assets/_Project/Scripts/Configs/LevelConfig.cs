using UnityEngine;

namespace TowerDefense.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "_Assets/_Project/Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [Header("Difficulty")]
        [SerializeField] private float enemySpawnTime = 1f;
        [SerializeField] private EnemyData enemyData;

        public float EnemySpawnTime => enemySpawnTime;
        public EnemyData EnemyData => enemyData;
    }
}