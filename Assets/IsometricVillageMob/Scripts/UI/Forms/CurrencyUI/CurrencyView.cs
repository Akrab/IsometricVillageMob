using TMPro;
using UnityEngine;

namespace IsometricVillageMob.UI.Forms.Currency
{
    public class CurrencyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;
        
        public void SetValue(int value)
        {
            _label.text = value.ToString();
        }
        
    }
}
