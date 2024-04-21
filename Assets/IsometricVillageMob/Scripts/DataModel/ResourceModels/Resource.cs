using System;
using IsometricVillageMob.Game;
using UnityEngine;

namespace IsometricVillageMob.DataModel
{
    [CreateAssetMenu(fileName = "ResourceModel", menuName = "ScriptableObject/New Resources")]
    public class Resource : BaseModel
    {


        [SerializeField] private ResourceData[] _data;

        public ResourceData[] Data => _data;


    }

    [Serializable]
    public class ResourceData
    {
        [SerializeField] private string _name;
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private Sprite _icon;
        [Min(0.5f)]
        [SerializeField] private float _durationSec;

        public string Name => _name;
        public ResourceType ResourceType => _resourceType;
        public Sprite Icon => _icon;
        public float Duration => _durationSec;

    }
}