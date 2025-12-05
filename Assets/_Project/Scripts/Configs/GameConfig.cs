using UnityEngine;

namespace TowerDefense.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "_Assets/_Project/Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Economy")]

        [SerializeField] private int startCurrency;

        [Header("Combat")]

        [SerializeField] private string[] startPlayerTowers = new string[]
        {
            "tower_archer",  // Лучник
            "tower_bomber"   // Подрывник
        };

        public int StartCurrency => startCurrency;
        public string[] StartPlayerTowers => startPlayerTowers;
    }
}