using UnityEngine;
using System;
using TowerDefense.Configs;
using Zenject;
using TowerDefense.Signals;

namespace TowerDefense.Gameplay.Managers
{
    public class CurrencyManager : MonoBehaviour
    {
        private SignalBus _signalBus;
        private GameConfig _gameConfig;
        private int _currency;

        public int Currency => _currency;

        [Inject]
        public void Construct(GameConfig gameConfig, SignalBus signalBus)
        {
            _gameConfig = gameConfig;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            AddCurrency(_gameConfig.StartCurrency);
        }

        public bool HasEnough(int amount)
        {
            return _currency >= amount;
        }

        public void AddCurrency (int amount)
        {
            ChangeCurrency(amount);
        }

        public void SpendCurrency (int amount)
        {
            if (!HasEnough(amount))
            {
                return;
            }
            ChangeCurrency(-amount);
        }
        private void ChangeCurrency(int amount)
        {
            _currency += amount;
            var signal = new OnCurrencyChanged();
            _signalBus.Fire(signal);
        }
    }
}