using System;
using UnityEngine.Scripting;

namespace IsometricVillageMob.DIIsometric
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class InjectAttribute : PreserveAttribute
    {

    }
}