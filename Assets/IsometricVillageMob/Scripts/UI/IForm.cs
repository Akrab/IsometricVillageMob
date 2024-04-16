using DG.Tweening;
using UnityEngine;

namespace IsometricVillageMob.IsometricVillageMob.UI
{
    public interface IForm
    {
        bool IsShow { get; }
        GameObject gameObject { get; }
        Tween Show(bool instance = false);
        Tween Hide(bool instance = false);
        void Disable();
        void Enable();
    }
}