using IsometricVillageMob.Game;
using UnityEngine;

namespace IsometricVillageMob.DataModel.Items
{
    public interface IItemModel
    {
        ItemType ItemType { get; }
        Sprite Icon { get; }
        public int Cost { get; }
    }
    
    public class ItemModel<T> : IItemModel where T : ItemData
    {
        private readonly T _data;
        public ItemType ItemType => _data.ItemType;
        public Sprite Icon => _data.Icon;
        public int Cost => _data.Cost;

        public ItemModel(T data)
        {
            _data = data;
        }
    }
}