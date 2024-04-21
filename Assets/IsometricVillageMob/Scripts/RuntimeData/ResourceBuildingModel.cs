using System.Collections.Generic;
using IsometricVillageMob.DataModel;
using IsometricVillageMob.Game;
using UnityEngine;

namespace IsometricVillageMob.RuntimeData
{

    public interface IResourceBuildingViewModel
    {
        public bool IsEmpty { get; }

        public string Name { get; }
        public Sprite Icon { get; }
        public float Duration { get; }
        public ResourceType CurrentResource { get; }
    }

    public class ResourceBuildingModel : IResourceBuildingViewModel
    {
        public ResourceNext ResourceNext = new ();
        
        public IResourceModel ResourceModel;
        public bool IsEmpty => ResourceModel == null;
        public string Name => ResourceModel?.Name ?? "";
        public Sprite Icon => ResourceModel?.Icon;
        public float Duration => ResourceModel?.Duration ?? 0;
        public ResourceType CurrentResource => ResourceModel?.ResourceType ?? ResourceType.None;
        
    }
}