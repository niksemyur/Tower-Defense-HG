using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TowerDefense.Gameplay.Managers;
using TowerDefense.Configs;

namespace TowerDefense.UI
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _towerNameTxt;
        [SerializeField] private TextMeshProUGUI _towerCostTxt;

        private int _towerCost;
        private TowerData _towerData;
        private Button _button;

        private void Awake ()
        {
            _button = GetComponent<Button>();
            CurrencyManager.OnCurrencyChanged += CheckState;
            GridManager.OnGridChanged += CheckState;
        }

        public void Init (TowerData towerData)
        {
            _towerData = towerData;
            _towerCost = _towerData.TowerCost;
            _towerNameTxt.text = towerData.TowerName;
            _towerCostTxt.text = _towerCost.ToString();
        }

        public void Select ()
        {
            TowerBuilder.Instance.BuildTower(_towerData);
        }

        private void CheckState()
        {
            _button.interactable = CurrencyManager.Instance.HasEnough(_towerCost) && GridManager.Instance.IsHaveEmptyCells();
        }

        private void OnDestroy()
        {
            if (CurrencyManager.Instance != null)
            {
                CurrencyManager.OnCurrencyChanged -= CheckState;
            }
            if (GridManager.Instance != null)
            {
                GridManager.OnGridChanged -= CheckState;
            }
        }
    }
}
