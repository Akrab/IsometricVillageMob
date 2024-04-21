using System.Collections.Generic;
using IsometricVillageMob.DataModel;
using IsometricVillageMob.DataModel.Items;
using IsometricVillageMob.DataModel.Merge.MergeTree;
using IsometricVillageMob.Game;
using UnityEngine;

namespace IsometricVillageMob.RuntimeData
{
    public class ManufactureBuildingModel
    {
        public IMergeTree MergeTree;
        public IItemModel ItemModel;
        public IResourceModel[] ResourceModels;

        public ResourceNext[] ResourceNext = new[] { new ResourceNext(), new ResourceNext() };
        public List<ResourceType> ResTypes = new List<ResourceType>();
        
        public ResourceType[] SelectResources = new[] { ResourceType.None, ResourceType.None };
        
        public ItemType ItemType => MergeTree?.ItemType ?? ItemType.None;
        
        public float Duration => MergeTree?.Duration ?? 0;
        public Sprite Icon => ItemModel?.Icon;


        public bool IsEmpty => MergeTree == null;

    }
}