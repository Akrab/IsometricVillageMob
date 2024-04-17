using UnityEngine;

namespace IsometricVillageMob.Infrastructure.Controllers.Inputs
{
    public class TouchInput : IInput
    {
        public bool Clicked => Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began;

        public Vector2 Get()
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                return Input.touches[0].position;
            }
            
            return Vector2.zero;
        }
    }
}