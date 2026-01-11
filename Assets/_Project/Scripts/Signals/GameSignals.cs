using TowerDefense.Configs;
namespace TowerDefense.Signals
{
    public struct RewardMoneySignal // добавляет награду за убийство врагов 
    {
        public int RewardAmount;
    } 
    public struct OnGridChangedSignal { } // изменение слотов сетки
    public struct OnCurrencyChangedSignal { } // изменение валюты
    public struct TowerSelectedSignal
    {
        public string TowerId;
    } // выбор башни
}

