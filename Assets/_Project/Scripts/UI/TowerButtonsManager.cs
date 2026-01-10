using UnityEngine;
using TowerDefense.Configs;

namespace TowerDefense.UI
{
    public class TowerButtonsManager : MonoBehaviour
    {
        [SerializeField] private TowerButton[] _towerButtons;
        public void Init(GameConfig gameConfig)
        {     
            if (gameConfig.PlayerTowers == null || gameConfig.PlayerTowers.Length < _towerButtons.Length)
            {
                Debug.LogError("Башни не назначены в GameConfig!");
                return;
            }

            for (int i = 0; i < _towerButtons.Length; i++)
            {
                _towerButtons[i].Init(gameConfig.PlayerTowers[i]);
            }
        }
    }
}
