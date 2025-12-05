using UnityEngine;
using System;

namespace TowerDefense.Configs
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "_Assets/_Project/Configs/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private EnemyInfo[] enemies;

        public EnemyInfo GetEnemyInfo(string enemyId)
        {
            // Ћинейный поиск - O(n), ок дл€ малого количества типов врагов
            // ƒл€ 50+ типов стоит использовать Dictionary дл€ O(1) поиска
            foreach (var enemy in enemies)
            {
                if (enemy.Id == enemyId)
                    return enemy;
            }

            Debug.LogError($"Enemy with ID '{enemyId}' not found!");
            return null;
        }

    }

    [Serializable]
    public class EnemyInfo
    {
        [Header("Info")]
        [SerializeField] private string name;
        [SerializeField] private string id;

        [Header("Parameters")]
        [SerializeField] private float health;
        [SerializeField] private int reward;

        [Header("Prefab")]
        [SerializeField] private GameObject enemyPrefab;
        public string Name => name;
        public string Id => id;
        public float Health => health;
        public int Reward => reward;
        public GameObject EnemyPrefab => enemyPrefab;
    }
}