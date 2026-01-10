using UnityEngine;
using TowerDefense.Configs;
using TowerDefense.Gameplay.Grid;

namespace TowerDefense.Gameplay.Managers
{
    public class TowerBuilder : MonoBehaviour
    {
        [Header("Containers")]
        [SerializeField] private Transform _towersContainer;
        [Header("Factory")]
        [SerializeField] private TowerFactory _towerFactory;
        public static TowerBuilder Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void BuildTower(TowerData towerData)
        {
            // 1. Проверить валюту
            int towerCost = towerData.TowerCost;
            if (!CurrencyManager.Instance.HasEnough(towerCost))
            {
                Debug.Log("Not enough currency");
                return;
            }

            // 2. Получить свободную ячейку
            GridCell emptyCell = GridManager.Instance.GetRandomEmptyCell();
            if (emptyCell == null)
            {
                Debug.Log("No empty cells available");
                return;
            }

            // 3. Списать валюту
            CurrencyManager.Instance.SpendCurrency(towerCost);

            // 4. Занять ячейку
            GridManager.Instance.SetGridBusyState(emptyCell, true);

            // 5. Создать башню
            var tower = _towerFactory.CreateTower
            (
            towerData,
            emptyCell.transform,
            _towersContainer   
            );

            if (tower == null)
            {
                Debug.LogWarning("Не удалось создать башню - " + towerData.name + "");
            }
        }
    }
}