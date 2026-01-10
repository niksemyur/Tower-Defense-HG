using UnityEngine;
using TowerDefense.Entities.Projectiles;

namespace TowerDefense.Configs
{
    [CreateAssetMenu(fileName = "TowersData", menuName = "_Assets/_Project/Configs/Towers/TowersData")]
    public class TowerData : ScriptableObject
    {
        [Header("Info")]
        [SerializeField] private string towerName;
        [SerializeField] private int cost;

        [Header("Combat")]
        [SerializeField] private float range = 5f; // Радиус атаки 
        [SerializeField] private float attackCooldown = 1f; // Перезарядка 
        [SerializeField] private float damage = 10f; // Урон
        [SerializeField] private float projectileSpeed = 10f; // Скорость снаряда
        [SerializeField] private float projectileRange = 1f;
        [SerializeField] private BaseProjectile projectile; // Префаб Санярда

        [Header("Prefab")]
        [SerializeField] private GameObject towerPrefab;
        public string TowerName => towerName;
        public int Cost => cost;
        public GameObject TowerPrefab => towerPrefab;
        public float Range => range;
        public float AttackCooldown => attackCooldown;
        public float Damage => damage;
        public float ProjectileSpeed => projectileSpeed;
        public float ProjectileRange => projectileRange;
        public BaseProjectile Projectile => projectile;
    }
}