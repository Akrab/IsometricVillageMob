﻿using UnityEngine;

namespace IsometricVillageMob.Infrastructure.Controllers.Inputs
{
    public class MouseInput : IInput
    {
        public bool Clicked => Input.GetMouseButtonDown(0);

        public Vector2 Get()
        {
            return Input.mousePosition;
        }
    }
}