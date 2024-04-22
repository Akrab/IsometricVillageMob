using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IsometricVillageMob.UI.Forms.Currency
{
    public class CurrencyView : MonoBehaviour
    {
        [SerializeField] private Image _ico;
        [SerializeField] private TextMeshProUGUI _label;

        private int count = -1;

        private Sequence _sequence;

        private void Anim(int newCount)
        {
            if (count == -1)
            {
                count = newCount;
                return;
            }

            _sequence?.Complete();
            _sequence = DOTween.Sequence();
            _ico.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
            _sequence.Append(_ico.transform.DOScale(Vector3.one * 1.3f, 0.2f).SetEase(Ease.Linear));
            _sequence.Append(_ico.transform.DOScale(Vector3.one, 0.1f));
            count = newCount;
        }

        public void SetValue(int value)
        {
            _label.text = value.ToString();
            Anim(value);
        }

        public void SetIcon(Sprite icon)
        {
            _ico.sprite = icon;
        }
    }
}
