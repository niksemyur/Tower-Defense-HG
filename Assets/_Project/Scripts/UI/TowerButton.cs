using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TowerDefense.Gameplay.Managers;

namespace TowerDefense.UI
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI towerNameTxt;
        [SerializeField] private TextMeshProUGUI costTxt;

        private string _towerId;
        private int _cost;

        private Button _button;
        public void Init()
        {
            _button = GetComponent<Button>();
            CurrencyManager.Instance.OnCurrencyChanged += CheckState;
            GridManager.Instance.OnGridChanged += CheckState;
        }

        public void Load(string towerId, int cost, string towerName)
        {
            _towerId = towerId;
            _cost = cost;

            towerNameTxt.text = towerName;
            costTxt.text = _cost.ToString();
        }

        public void Select ()
        {
            TowerBuilder.Instance.BuildTower(_towerId);
        }

        private void CheckState()
        {
            _button.interactable = CurrencyManager.Instance.HasEnough(_cost) && GridManager.Instance.IsHaveEmptyCells();
        }

        private void OnDestroy()
        {
            if (CurrencyManager.Instance != null)
            {
                CurrencyManager.Instance.OnCurrencyChanged -= CheckState;
            }
            if (GridManager.Instance != null)
            {
                GridManager.Instance.OnGridChanged -= CheckState;
            }
        }
    }
}
