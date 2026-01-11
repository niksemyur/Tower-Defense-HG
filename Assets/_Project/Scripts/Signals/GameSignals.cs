namespace TowerDefense.Signals
{
    public struct RewardMoneySignal // добавляет награду за убийство врагов 
    {
        public int RewardAmount;
    } 
    public struct OnGridChanged { } // изменение слотов сетки
    public struct OnCurrencyChanged { } // изменение валюты
}

