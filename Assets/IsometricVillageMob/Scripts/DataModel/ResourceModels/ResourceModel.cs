using IsometricVillageMob.Game;
using UnityEngine;

namespace IsometricVillageMob.DataModel
{
    public interface IResourceModel
    {
        ResourceType ResourceType { get; }
        Sprite Icon { get; }
        float Duration { get; } 
        string Name { get; }
    }

    
    public class ResourceModel<T> : IResourceModel where T : ResourceData
    {
        private readonly T _data;
        public ResourceType ResourceType => _data.ResourceType;
        public Sprite Icon => _data.Icon;
        public float Duration => _data.Duration;
        public string Name => _data.Name;

        public ResourceModel(T data)
        {
            _data = data;
        }
    }
}