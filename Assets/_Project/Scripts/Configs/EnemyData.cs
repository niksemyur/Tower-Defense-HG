using UnityEngine;
using System;

namespace TowerDefense.Configs
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "_Assets/_Project/Configs/Enemies/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [Header("Info")]
        [SerializeField] private string _enemyName;

        [Header("Parameters")]
        [SerializeField] private float _health;
        [SerializeField] private int _reward;

        [Header("Prefab")]
        [SerializeField] private GameObject _enemyPrefab;
        public string EnemyName => _enemyName;
        public float Health => _health;
        public int Reward => _reward;
        public GameObject EnemyPrefab => _enemyPrefab;

    }
}