using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TowerDefense.Configs;
using TowerDefense.Gameplay.Managers;
using Zenject;
using TowerDefense.Signals;

namespace TowerDefense.UI
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _towerNameTxt;
        [SerializeField] private TextMeshProUGUI _towerCostTxt;

        private int _towerCost;
        private string _towerId;
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

            _signalBus.Subscribe<OnGridChangedSignal>(CheckState);
            _signalBus.Subscribe<OnCurrencyChangedSignal>(CheckState);

            _towerId = towerData.TowerId;
            _towerCost = towerData.TowerCost;
            _towerNameTxt.text = towerData.TowerName;
            _towerCostTxt.text = _towerCost.ToString();
        }

        public void Select ()
        {
            _signalBus.Fire(new TowerSelectedSignal
            {
                TowerId = _towerId
            });
        }

        private void CheckState()
        {
            _button.interactable = _currencyManager.HasEnough(_towerCost) && _gridManager.IsHaveEmptyCells();
        }

        private void OnDestroy()
        {
            _signalBus?.TryUnsubscribe<OnGridChangedSignal>(CheckState);
            _signalBus?.TryUnsubscribe<OnCurrencyChangedSignal>(CheckState);
        }
    }
}
