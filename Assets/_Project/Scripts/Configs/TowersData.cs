using UnityEngine;
using System;

namespace TowerDefense.Configs
{
    [CreateAssetMenu(fileName = "TowersData", menuName = "_Assets/_Project/Configs/TowersData")]
    public class TowersData : ScriptableObject
    {
        [SerializeField] private TowerInfo[] towers;

        public TowerInfo GetTowerInfo(string towerId)
        {    
            // Такой же поиск как у врагов. Если башен станет много - переделать на Dictionary
            foreach (var tower in towers)
            {
                if (tower.Id == towerId)
                    return tower;
            }

            Debug.LogError($"Tower with ID '{towerId}' not found!");
            return null;
        }

    }

    [Serializable]
    public class TowerInfo
    {
        [Header ("Info")]

        [SerializeField] private string name;
        [SerializeField] private string id;
        [SerializeField] private int cost;

        [Header("Prefab")]
        [SerializeField] private GameObject towerPrefab;
        public string Name => name;
        public string Id => id;
        public int Cost => cost;
        public GameObject TowerPrefab => towerPrefab;
    }
}