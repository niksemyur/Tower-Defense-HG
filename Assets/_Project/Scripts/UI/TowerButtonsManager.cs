using UnityEngine;
using TowerDefense.Configs;
using Zenject;

namespace TowerDefense.UI
{
    public class TowerButtonsManager : MonoBehaviour
    {
        [SerializeField] private TowerButton[] _towerButtons;

        private GameConfig _gameConfig;

        [Inject]
        public void Construct(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }
        public void Initialize()
        {     
            if (_gameConfig.PlayerTowers == null || _gameConfig.PlayerTowers.Length < _towerButtons.Length)
            {
                Debug.LogError("Башни не назначены в GameConfig!");
                return;
            }

            for (int i = 0; i < _towerButtons.Length; i++)
            {
                _towerButtons[i].Initialize(_gameConfig.PlayerTowers[i]);
            }
        }
    }
}
