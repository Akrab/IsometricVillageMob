using System.Collections.Generic;
using IsometricVillageMob.DataModel;
using IsometricVillageMob.Game;

namespace IsometricVillageMob.Services

{
    public interface ICurrencyService
    {
        IReadOnlyList<ICurrencyModel> GetCurrencies();
        ICurrencyModel Get(CurrencyType currencyType);
    }
}