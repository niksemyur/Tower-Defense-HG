using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TowerDefense.Gameplay.Managers;
using TowerDefense.Configs;

namespace TowerDefense.UI
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI towerNameTxt;
        [SerializeField] private TextMeshProUGUI costTxt;

        private TowerData towerData;
        private int cost;

        private Button button;
        public void Init()
        {
            button = GetComponent<Button>();
            CurrencyManager.Instance.OnCurrencyChanged += CheckState;
            GridManager.Instance.OnGridChanged += CheckState;
        }

        public void Load(TowerData _towerData)
        {
            towerData = _towerData;

            cost = towerData.Cost;
            towerNameTxt.text = towerData.TowerName;
            costTxt.text = cost.ToString();
        }

        public void Select ()
        {
            TowerBuilder.Instance.BuildTower(towerData);
        }

        private void CheckState()
        {
            button.interactable = CurrencyManager.Instance.HasEnough(cost) && GridManager.Instance.IsHaveEmptyCells();
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
