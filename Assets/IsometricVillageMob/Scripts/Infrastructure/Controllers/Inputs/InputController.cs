using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace IsometricVillageMob.Infrastructure.Controllers.Inputs
{
    public class ClickEvent : UnityEvent <Transform> {}
    public interface IInputController
    {
        void AddListener(UnityAction<Transform> action);
        void RemoveListener(UnityAction<Transform> action);
    }
    public class InputController : ITickable, IInputController
    {
        private LayerMask _layerMask;
        private IInput _input;
        private Camera _camera;
        private ClickEvent _event = new ();
        private Camera Camera
        {
            get
            {
                if (!_camera) _camera = Camera.main;
                return _camera;
            }
        }

        public InputController()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            _input = new MouseInput();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
            _input = new TouchInput();
#endif
            
            _layerMask = LayerMask.GetMask(CONSTANTS.BUILDER_CLICK_LAYER_MASK);
        }

        public void AddListener(UnityAction<Transform> action)
        {
            _event.AddListener(action);
        }

        public void Tick()
        {
            if (!Camera) return;
            if (!_input.Clicked) return;
            
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            
            var hit = Physics2D.GetRayIntersection(Camera.ScreenPointToRay(_input.Get()), float.PositiveInfinity, _layerMask);
            if (hit.collider)
            {
                _event?.Invoke(hit.collider.transform);
            }

        }

        public void RemoveListener(UnityAction<Transform> action)
        {
            _event.RemoveListener(action);
        }


    }
}