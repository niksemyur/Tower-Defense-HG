using UnityEngine;
using TowerDefense.Configs;

namespace TowerDefense.UI
{
    public class GameUIController : MonoBehaviour
    {
        [Header ("UI Managers")]
        [SerializeField] private CurrencyDisplay _currencyDisplay;
        [SerializeField] private TowerButtonsManager _towerButtonsManager;

        public void Init(GameConfig gameConfig)
        {
            _towerButtonsManager.Init(gameConfig);
        }
    }
}
