using UnityEngine;
using System;
using TowerDefense.Configs;

namespace TowerDefense.Gameplay.Managers
{
    public class CurrencyManager : MonoBehaviour
    {
        public static CurrencyManager Instance { get; private set; }

        private GameConfig _gameConfig;
        private int _currency;

        public int Currency => _currency;
        public event Action OnCurrencyChanged;

        public void Init(GameConfig config)
        {
            _gameConfig = config;

            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void Load()
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
            OnCurrencyChanged?.Invoke();

        }
    }
}