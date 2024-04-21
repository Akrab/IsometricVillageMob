using IsometricVillageMob.Game;
using UnityEngine;

namespace IsometricVillageMob.DataModel
{
    public interface ICurrencyModel
    {
        CurrencyType CurrencyType { get; }
        Sprite Icon { get; }
    }

    public class CurrencyModel<T> : ICurrencyModel where T : CurrencyData
    {
        private readonly T _data;
        public CurrencyType CurrencyType => _data.CurrencyType;
        public Sprite Icon => _data.Icon;

        public CurrencyModel(T data)
        {
            _data = data;
        }
    }

}