using System.Collections.Generic;
using IsometricVillageMob.DataModel;
using IsometricVillageMob.Game;
using IsometricVillageMob.Infrastructure.Containers;

namespace IsometricVillageMob.Services
{
    public class CurrencyService: ICurrencyService
    {
        private List<ICurrencyModel> _models;

        public CurrencyService(ModelContainer modelContainer)
        {
            var currencies = modelContainer.Get<Currencies>().Data;
            _models = new List<ICurrencyModel>(currencies.Length);
            foreach (var item in currencies)
                _models.Add(new CurrencyModel<CurrencyData>(item));
        }
        
        public IReadOnlyList<ICurrencyModel> GetCurrencies()
        {
            return _models;            
        }

        public ICurrencyModel Get(CurrencyType currencyType)
        {
            return _models.Find(D => D.CurrencyType == currencyType);
        }
    }
}