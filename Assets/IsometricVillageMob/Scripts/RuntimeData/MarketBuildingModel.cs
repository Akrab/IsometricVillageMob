using IsometricVillageMob.DataModel.Items;
using IsometricVillageMob.Game;

namespace IsometricVillageMob.RuntimeData
{

    public interface IMarketBuildingModel
    {
        IItemModel ItemModel { get; }
        int Count { get; }
    }
    
    public class MarketBuildingModel : IMarketBuildingModel
    {
        public EnumNextValue<ItemType> EnumNextValue = new();
        public IItemModel ItemModel { get; set; }
        public int Count { get; set; }
    }
}