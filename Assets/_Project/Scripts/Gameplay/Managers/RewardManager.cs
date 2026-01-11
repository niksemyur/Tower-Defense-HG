using UnityEngine;
using TowerDefense.Signals;
using Zenject;

namespace TowerDefense.Gameplay.Managers
{
    // Выдаёт награды за убийство врагов
    public class RewardManager : MonoBehaviour
    {
        private CurrencyManager _currencyManager;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(CurrencyManager currencyManager, SignalBus signalBus)
        {
            _currencyManager = currencyManager;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<RewardMoneySignal>(OnRewardMoney);
        }

        private void OnDestroy()
        {
            _signalBus?.TryUnsubscribe<RewardMoneySignal>(OnRewardMoney);
        }

        private void OnRewardMoney(RewardMoneySignal signal)
        {
            _currencyManager.AddCurrency(signal.RewardAmount);
        }
    }
}