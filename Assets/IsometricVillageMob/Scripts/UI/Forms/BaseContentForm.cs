using DG.Tweening;
using IsometricVillageMob.UI.CustomComponents;
using UnityEngine;

namespace IsometricVillageMob.UI.Forms
{
    public class BaseContentForm : BaseForm
    {
        private const float CONTENT_SHOW_DURATION = 0.15f;
      
        [SerializeField] private RectTransform _contentRoot;
        [SerializeField] protected ButtonExt _btnClose;
        protected void OnClose()
        {
            //_gameStateMachine.EnterToState<M>()
            Hide();
        }
        
        public override Tween Show(bool instance = false)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(base.Show(instance));

            _contentRoot.localScale = Vector3.zero;

            sequence.Append(_contentRoot.DOScale(Vector3.one, CONTENT_SHOW_DURATION).SetEase(Ease.OutBack));
            return sequence;
        }
    }
}