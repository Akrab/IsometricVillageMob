using UnityEngine;

namespace IsometricVillageMob.Infrastructure.Controllers.Inputs
{
    public interface IInput
    {
        bool Clicked { get; }
        Vector2 Get();
    }
}