using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace IsometricVillageMob.UI.CustomComponents
{
    public class ButtonExt : Button
    {
        private float _pressedScale = 0.9f;
        private float _pressedDuration = 0.1f;
        private float _pressBackDuration = 0.1f;

        private Vector3 _pressed, _released;

        private bool _scalesInited;

        protected override void Awake()
        {
            base.Awake();
            InitScales();
        }

        private void InitScales()
        {
            if (_scalesInited)
                return;

            _scalesInited = true;
            _released = transform.localScale;
            _pressed = _released * _pressedScale;

        }


        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            InitScales();

            base.DoStateTransition(state, instant);

            switch (state)
            {
                case SelectionState.Pressed:
                    transform.DOScale(_pressed, _pressedDuration);
                    break;
                case SelectionState.Selected:
                case SelectionState.Highlighted:
                case SelectionState.Normal:
                    transform.DOScale(_released, _pressBackDuration);
                    break;
                default:
                    break;
            }
        }
    }
}