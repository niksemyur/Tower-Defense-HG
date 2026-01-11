using TMPro;
using TowerDefense.Gameplay.Managers;
using UnityEngine;
using TowerDefense.Signals;
using Zenject;

namespace TowerDefense.UI
{
    public class CurrencyDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currencyText;

        private SignalBus _signalBus;
        private CurrencyManager _currencyManager;

        [Inject]
        public void Construct(SignalBus signalBus, CurrencyManager currencyManager)
        {
            _signalBus = signalBus;
            _currencyManager = currencyManager;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<OnCurrencyChangedSignal>(UpdateDisplay);
        }

        private void UpdateDisplay()
        {
            _currencyText.text = _currencyManager.Currency.ToString();
        }

        private void OnDestroy()
        {
            _signalBus?.TryUnsubscribe<OnCurrencyChangedSignal>(UpdateDisplay);
        }
    }
}
