using System.Collections.Generic;
using IsometricVillageMob.DataModel;
using IsometricVillageMob.DataModel.Items;
using IsometricVillageMob.Game;
using IsometricVillageMob.Infrastructure.Containers;
using IsometricVillageMob.Services;

namespace IsometricVillageMob.IsometricVillageMob.Scripts.Services.Items
{
    public class ItemService : IItemService
    {
        private List<IItemModel> _models;

        public ItemService(ModelContainer modelContainer)
        {
            var items = modelContainer.Get<Item>().Data;
            _models = new List<IItemModel>(items.Length);
            foreach (var item in items)
                _models.Add(new ItemModel<ItemData>(item));
        }
        
        public IReadOnlyList<IItemModel> GetItems()
        {
            return _models;
        }

        public IItemModel Get(ItemType itemType)
        {
            return _models.Find(D => D.ItemType == itemType);
        }
    }
}
