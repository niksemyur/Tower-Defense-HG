using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TowerDefense.Gameplay.Managers;
using TowerDefense.Configs;
using Zenject;
using TowerDefense.Signals;

namespace TowerDefense.UI
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _towerNameTxt;
        [SerializeField] private TextMeshProUGUI _towerCostTxt;

        private int _towerCost;
        private TowerData _towerData;
        private Button _button;

        private SignalBus _signalBus;
        private CurrencyManager _currencyManager;
        private GridManager _gridManager;

        [Inject]
        public void Construct(SignalBus signalBus, CurrencyManager currencyManager, GridManager gridManager)
        {
            _signalBus = signalBus;
            _currencyManager = currencyManager;
            _gridManager = gridManager;
        }

        public void Initialize(TowerData towerData)
        {
            _button = GetComponent<Button>();

            _signalBus.Subscribe<OnGridChanged>(CheckState);
            _signalBus.Subscribe<OnCurrencyChanged>(CheckState);

            _towerData = towerData;
            _towerCost = _towerData.TowerCost;
            _towerNameTxt.text = towerData.TowerName;
            _towerCostTxt.text = _towerCost.ToString();
        }

        public void Select ()
        {
            TowerBuilder.Instance.BuildTower(_towerData);
        }

        private void CheckState()
        {
            _button.interactable = _currencyManager.HasEnough(_towerCost) && _gridManager.IsHaveEmptyCells();
        }

        private void OnDestroy()
        {
            _signalBus?.TryUnsubscribe<OnGridChanged>(CheckState);
            _signalBus?.TryUnsubscribe<OnCurrencyChanged>(CheckState);
        }
    }
}
