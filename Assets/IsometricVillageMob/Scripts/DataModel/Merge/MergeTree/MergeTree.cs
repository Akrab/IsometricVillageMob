using System;
using IsometricVillageMob.Game;
using UnityEngine;

namespace IsometricVillageMob.DataModel.Merge.MergeTree
{
    [CreateAssetMenu(fileName = "MergeTree", menuName = "ScriptableObject/New MergeTree" )]
    public class MergeTreas: BaseModel
    {
        [SerializeField] private TreeData[] _data;
        
        public TreeData[] Data=>_data;
    }

    [Serializable]
    public class TreeData
    {
        [SerializeField] private ItemType _item;
        [SerializeField] private float _duration;
        [SerializeField] private ResourceType[] _resources;
        
        public ItemType ItemType => _item;
        public float Duration => _duration;
        public ResourceType[] Resources => _resources;
    }
    
    public interface IMergeTree
    {
        ItemType ItemType { get; }
        public float Duration { get; }
        public ResourceType[] Resource { get; }
    }
    
    public class MergeTreeModel<T> : IMergeTree where T : TreeData
    {
        private readonly T _data;
        public ItemType ItemType => _data.ItemType;
        public float Duration => _data.Duration;
        public ResourceType[] Resource => _data.Resources;
        public MergeTreeModel(T data)
        {
            _data = data;
        }


    }
    
}