using UnityEngine;
using System;

namespace TowerDefense.Configs
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "_Assets/_Project/Configs/Enemies/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [Header("Info")]
        [SerializeField] private string enemyName;

        [Header("Parameters")]
        [SerializeField] private float health;
        [SerializeField] private int reward;

        [Header("Prefab")]
        [SerializeField] private GameObject enemyPrefab;
        public string EnemyName => enemyName;
        public float Health => health;
        public int Reward => reward;
        public GameObject EnemyPrefab => enemyPrefab;

    }
}