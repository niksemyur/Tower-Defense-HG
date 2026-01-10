using UnityEngine;
using TowerDefense.Configs;

namespace TowerDefense.UI
{
    public class GameUIController : MonoBehaviour
    {
        [Header ("UI Managers")]
        [SerializeField] private CurrencyDisplay currencyDisplay;
        [SerializeField] private TowerButtonsManager towerButtonsManager;

        public void Init()
        {
            currencyDisplay.Init();
            towerButtonsManager.Init();
        }

        public void Load (GameConfig gameConfig)
        {
            towerButtonsManager.Load(gameConfig);
        }
    }
}
