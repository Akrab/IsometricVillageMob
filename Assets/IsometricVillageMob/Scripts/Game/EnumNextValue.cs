using System;
using System.Collections.Generic;

namespace IsometricVillageMob.Game
{
    public class EnumNextValue<T> where T : struct
    {
        private const string NONE = "None";
        private List<T> _data;

        private T _currentType;
        private readonly T _noneType;

        public EnumNextValue()
        {
            _data = new List<T>((T[])Enum.GetValues(typeof(T)));
            _noneType = Enum.Parse<T>(NONE);
            _data.Remove(_noneType);
            _currentType = _noneType;
        }

        public T Next()
        {
            int index = 0;
            if (!Equals(_currentType, _noneType))
                index =_data.IndexOf(_currentType) + 1;
            
            if (index >= _data.Count)
                index = 0;
            
            return _currentType = _data[index];

        }
    }
}