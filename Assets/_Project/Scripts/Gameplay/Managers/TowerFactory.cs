using UnityEngine;
using TowerDefense.Entities.Towers;
using TowerDefense.Configs;

namespace TowerDefense.Gameplay.Managers
{
    public class TowerFactory
    {
        public BaseTower CreateTower(TowerData towerData, Transform spawnPoint, Transform towersContainer)
        {
            if (towerData.TowerPrefab == null)
            {
                Debug.LogError($"Префаб башни '{towerData.name}' не назначен!");
                return null;
            }

            GameObject towerObject = Object.Instantiate(
                towerData.TowerPrefab,
                spawnPoint.position,
                spawnPoint.rotation,
                towersContainer
            );

            BaseTower tower = towerObject.GetComponent<BaseTower>();
            if (tower == null)
            {
                Debug.LogError($"Префаб башни '{towerData.name}' не содержит компонент BaseTower!");
                Object.Destroy(towerObject);
                return null;
            }

            return tower;
        }
    }
}