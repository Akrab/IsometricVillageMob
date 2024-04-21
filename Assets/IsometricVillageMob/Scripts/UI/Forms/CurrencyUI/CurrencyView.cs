using IsometricVillageMob.Game;
using IsometricVillageMob.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IsometricVillageMob.UI.Forms.Currency
{
    public class CurrencyView : MonoBehaviour
    {
        [SerializeField] private Image _ico;
        [SerializeField] private TextMeshProUGUI _label;
        
        public void SetValue(int value)
        {
            _label.text = value.ToString();
        }

        public void SetIcon(Sprite icon)
        {
            _ico.sprite = icon;
        }
    }
}
