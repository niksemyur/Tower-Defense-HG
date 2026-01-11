using UnityEngine;

namespace TowerDefense.UI
{
    public class GameUIController : MonoBehaviour
    {
        [Header ("UI Managers")]
        [SerializeField] private CurrencyDisplay _currencyDisplay;
        [SerializeField] private TowerButtonsManager _towerButtonsManager;

        public void Initialize()
        {
            _towerButtonsManager.Initialize();
            _currencyDisplay.Initialize();
        }
    }
}
