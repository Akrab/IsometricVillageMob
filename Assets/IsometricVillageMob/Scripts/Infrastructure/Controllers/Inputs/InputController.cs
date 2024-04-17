using System;
using UnityEngine;

namespace IsometricVillageMob.Infrastructure.Controllers.Inputs
{
    public class InputController : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _layerMask;
        private IInput _input;
        private Camera _camera;
        
        private Camera Camera
        {
            get
            {
                if (!_camera) _camera = Camera.main;
                return _camera;
            }
        }

        private void Awake()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            _input = new MouseInput();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
            _input = new TouchInput();
#endif
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!Camera) return;
            if (!_input.Clicked) return;

            
            var hit = Physics2D.GetRayIntersection(Camera.ScreenPointToRay(_input.Get()), float.PositiveInfinity, _layerMask);
            if (hit.collider)
            {
                Debug.LogError(hit.collider.transform.name);
            }
            // var hit = Physics2D.Raycast(_input.Get(), Vector2.zero);
            // if (hit.collider)
            // {
            //     Debug.LogError(hit.collider.transform.name);
            // }
        }
    }
}