using UnityEngine;
using TowerDefense.Entities.Projectiles;

namespace TowerDefense.Configs
{
    [CreateAssetMenu(fileName = "TowersData", menuName = "_Assets/_Project/Configs/Towers/TowersData")]
    public class TowerData : ScriptableObject
    {
        [Header("Info")]
        [SerializeField] private string _towerName;
        [SerializeField] private string _towerId;
        [SerializeField] private int _towerCost;

        [Header("Combat")]
        [SerializeField] private float _range = 5f; // Радиус атаки 
        [SerializeField] private float _attackCooldown = 1f; // Перезарядка 
        [SerializeField] private float _damage = 10f; // Урон
        [SerializeField] private float _projectileSpeed = 10f; // Скорость снаряда
        [SerializeField] private float _projectileRange = 1f;
        [SerializeField] private BaseProjectile _projectile; // Префаб Санярда

        [Header("Prefab")]
        [SerializeField] private GameObject _towerPrefab;
        public string TowerName => _towerName;
        public string TowerId => _towerId;
        public int TowerCost => _towerCost;
        public GameObject TowerPrefab => _towerPrefab;
        public float Range => _range;
        public float AttackCooldown => _attackCooldown;
        public float Damage => _damage;
        public float ProjectileSpeed => _projectileSpeed;
        public float ProjectileRange => _projectileRange;
        public BaseProjectile Projectile => _projectile;
    }
}