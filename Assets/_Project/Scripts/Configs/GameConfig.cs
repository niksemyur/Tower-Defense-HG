using UnityEngine;

namespace TowerDefense.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "_Assets/_Project/Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Economy")]

        [SerializeField] private int startCurrency;

        [Header("Combat")]

        [SerializeField] private TowerData[] playerTowers;

        public int StartCurrency => startCurrency;
        public TowerData[] PlayerTowers => playerTowers;
    }
}