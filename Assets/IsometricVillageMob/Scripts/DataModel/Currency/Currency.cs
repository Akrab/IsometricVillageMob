using System;
using IsometricVillageMob.Game;
using UnityEngine;

namespace IsometricVillageMob.DataModel
{
    [CreateAssetMenu(fileName = "CurrencyModel", menuName = "ScriptableObject/New Currencies" )]
    public class Currencies : BaseModel
    {
        [SerializeField]
        private CurrencyData[] _data;

        public CurrencyData[] Data => _data;
    }

    [Serializable]
    public class CurrencyData
    {
        [SerializeField] private string _name;
        [SerializeField] private CurrencyType _currencyType;
        [SerializeField] private Sprite _icon;


        public string Name => _name;
        public CurrencyType CurrencyType => _currencyType;
        public Sprite Icon => _icon;
    }
}