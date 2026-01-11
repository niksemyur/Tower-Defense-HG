using UnityEngine;
using TowerDefense.Configs;
using TowerDefense.Gameplay.Grid;
using Zenject;

namespace TowerDefense.Gameplay.Managers
{
    public class TowerBuilder : MonoBehaviour
    {
        [Header("Containers")]
        [SerializeField] private Transform _towersContainer;

        private TowerFactory _towerFactory;
        private CurrencyManager _currencyManager;
        private GridManager _gridManager;
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

        [Inject]
        public void Construct(TowerFactory towerFactory, CurrencyManager currencyManager, GridManager gridManager)
        {
            _towerFactory = towerFactory;
            _currencyManager = currencyManager;
            _gridManager = gridManager;
        }

        public void BuildTower(TowerData towerData)
        {
            // 1. Проверить валюту
            int towerCost = towerData.TowerCost;
            if (!_currencyManager.HasEnough(towerCost))
            {
                Debug.Log("Not enough currency");
                return;
            }

            // 2. Получить свободную ячейку
            GridCell emptyCell = _gridManager.GetRandomEmptyCell();
            if (emptyCell == null)
            {
                Debug.Log("No empty cells available");
                return;
            }

            // 3. Списать валюту
            _currencyManager.SpendCurrency(towerCost);

            // 4. Занять ячейку
            _gridManager.SetGridBusyState(emptyCell, true);

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