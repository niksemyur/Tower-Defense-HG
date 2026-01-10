using UnityEngine;

namespace TowerDefense.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "_Assets/_Project/Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Economy")]

        [SerializeField] private int _startCurrency;

        [Header("Combat")]

        [SerializeField] private TowerData[] _playerTowers;

        public int StartCurrency => _startCurrency;
        public TowerData[] PlayerTowers => _playerTowers;
    }
}