using System.Collections.Generic;
using IsometricVillageMob.DataModel.Items;
using IsometricVillageMob.Game;

namespace IsometricVillageMob.Services
{
    public interface IItemService
    {
        IReadOnlyList<IItemModel> GetItems();

        IItemModel Get(ItemType itemType);
    }
}