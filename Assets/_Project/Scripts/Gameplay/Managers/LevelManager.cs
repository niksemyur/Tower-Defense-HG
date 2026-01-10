using TowerDefense.Configs;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Difficulty")]
    [SerializeField] private float enemySpawnTime = 1f;
    [SerializeField] private EnemyData enemyData;

    public float EnemySpawnTime => enemySpawnTime;
    public EnemyData EnemyData => enemyData;
}
