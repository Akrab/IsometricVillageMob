using System.Collections.Generic;
using IsometricVillageMob.DataModel;
using IsometricVillageMob.DataModel.Items;
using IsometricVillageMob.DataModel.Merge.MergeTree;
using IsometricVillageMob.Game;
using UnityEngine;

namespace IsometricVillageMob.RuntimeData
{
    public interface IManufactureBuildingModel
    {
        bool IsEmpty { get; }
        float Duration { get; }
        Sprite Icon { get; }
        
        IItemModel ItemModel { get; }

        IResourceModel ResourceModel(int index);
    }

    public class ManufactureBuildingModel : IManufactureBuildingModel
    {
        public IMergeTree MergeTree;
        public IItemModel ItemModel { get; set; }
        public IResourceModel[] ResourceModels = new IResourceModel[2] ;

        public ResourceNext[] ResourceNext = new[] { new ResourceNext(), new ResourceNext() };
        //public ResourceType[] SelectResources = new[] { ResourceType.None, ResourceType.None };
        
        public ItemType ItemType => MergeTree?.ItemType ?? ItemType.None;
        public float Duration => MergeTree?.Duration ?? 0;
        public Sprite Icon => ItemModel?.Icon;
 
        public bool IsEmpty => MergeTree == null;

        public IResourceModel ResourceModel(int index)
        {
            return ResourceModels?[index];
        }

    }
}