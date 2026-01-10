using TMPro;
using TowerDefense.Gameplay.Managers;
using UnityEngine;

namespace TowerDefense.UI
{
    public class CurrencyDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currencyText;

        private void Awake ()
        {
            CurrencyManager.OnCurrencyChanged += UpdateDisplay;
        }

        private void UpdateDisplay()
        {
            _currencyText.text = CurrencyManager.Instance.Currency.ToString();
        }

        private void OnDestroy()
        {
            if (CurrencyManager.Instance != null)
            {
                CurrencyManager.OnCurrencyChanged -= UpdateDisplay;
            }
        }
    }
}
