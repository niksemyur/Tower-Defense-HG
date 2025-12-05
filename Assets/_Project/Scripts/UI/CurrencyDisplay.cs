using TMPro;
using TowerDefense.Gameplay.Managers;
using UnityEngine;

namespace TowerDefense.UI
{
    public class CurrencyDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyText;

        public void Init()
        {
            CurrencyManager.Instance.OnCurrencyChanged += UpdateDisplay;
        }

        private void UpdateDisplay()
        {
            currencyText.text = CurrencyManager.Instance.Currency.ToString();
        }

        private void OnDestroy()
        {
            if (CurrencyManager.Instance != null)
            {
                CurrencyManager.Instance.OnCurrencyChanged -= UpdateDisplay;
            }
        }
    }
}
