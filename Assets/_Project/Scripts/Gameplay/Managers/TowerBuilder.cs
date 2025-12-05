using UnityEngine;
using TowerDefense.Configs;
using TowerDefense.Gameplay.Grid;

namespace TowerDefense.Gameplay.Managers
{
    public class TowerBuilder : MonoBehaviour
    {
        [Header("Containers")]
        [SerializeField] private Transform towersContainer;
        public static TowerBuilder Instance { get; private set; }

        private TowersData towersData;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void Init(TowersData data)
        {
            towersData = data;
        }

        public void BuildTower(string towerId)
        {
            // 1. Получить информацию о башне
            TowerInfo towerInfo = towersData.GetTowerInfo(towerId);
            if (towerInfo == null)
            {
                Debug.LogError($"Tower info not found for ID: {towerId}");
                return;
            }

            // 2. Проверить валюту
            if (!CurrencyManager.Instance.HasEnough(towerInfo.Cost))
            {
                Debug.Log("Not enough currency");
                return;
            }

            // 3. Получить свободную ячейку
            GridCell emptyCell = GridManager.Instance.GetRandomEmptyCell();
            if (emptyCell == null)
            {
                Debug.Log("No empty cells available");
                return;
            }

            // 4. Списать валюту
            CurrencyManager.Instance.SpendCurrency(towerInfo.Cost);

            // 5. Занять ячейку
            GridManager.Instance.SetGridBusyState(emptyCell, true);

            // 6. Получить префаб
            GameObject towerPrefab = towerInfo.TowerPrefab;
            if (towerPrefab == null)
            {
                Debug.LogError($"Prefab not found for tower ID: {towerId}");
                return;
            }

            // 7. Создать башню
            GameObject towerObj = Instantiate(towerPrefab, emptyCell.transform.position, Quaternion.identity, towersContainer);
        }
    }
}