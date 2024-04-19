
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IsometricVillageMob.UI.Forms.CurrencyUI
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _value;

        public void SetValue(int value)
        {
            _value.text = value.ToString();
        }
    }
}