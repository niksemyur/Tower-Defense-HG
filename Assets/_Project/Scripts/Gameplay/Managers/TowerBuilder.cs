using UnityEngine;
using TowerDefense.Configs;
using TowerDefense.Gameplay.Grid;
using TowerDefense.Signals;
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
        private SignalBus _signalBus;
        private TowerDataBase _towerDataBase;

        private TowerData AllTowers;

        [Inject]
        public void Construct(SignalBus signalBus, TowerFactory towerFactory, CurrencyManager currencyManager, GridManager gridManager, TowerDataBase towerDataBase)
        {
            _signalBus = signalBus;
            _towerFactory = towerFactory;
            _currencyManager = currencyManager;
            _gridManager = gridManager;
            _towerDataBase = towerDataBase;
        }

        public void Initialize ()
        {
            _signalBus.Subscribe<TowerSelectedSignal>(BuildTower);
        }

        public void BuildTower(TowerSelectedSignal signal)
        {   
            //1. Получаем конфиг башни
            TowerData towerData = _towerDataBase.GetTower(signal.TowerId);
            if (towerData == null)
            {
                return;
            }

            // 2. Проверить валюту
            int towerCost = towerData.TowerCost;
            if (!_currencyManager.HasEnough(towerCost))
            {
                Debug.Log("Не хватает валюты");
                return;
            }

            // 3. Получить свободную ячейку
            GridCell emptyCell = _gridManager.GetRandomEmptyCell();
            if (emptyCell == null)
            {
                Debug.Log("Нет свободных слотов");
                return;
            }

            // 4. Списать валюту
            _currencyManager.SpendCurrency(towerCost);

            // 5. Занять ячейку
            _gridManager.SetGridBusyState(emptyCell, true);

            // 6. Создать башню
            var tower = _towerFactory.CreateTower
            (
            towerData,
            emptyCell.transform,
            _towersContainer   
            );
        }

        private void OnDestroy()
        {
            _signalBus?.TryUnsubscribe<TowerSelectedSignal>(BuildTower);
        }
    }
}