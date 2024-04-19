using UnityEngine;

namespace IsometricVillageMob.Infrastructure.SaveLoad
{
    public class PrefsProvider
    {
        private string _key;
        private int _value = -1;
        public PrefsProvider(string key)
        {
            _key = key;
        }

        public void Set(int value)
        {
            if (value < 0) value = 0;
            _value = value;
            PlayerPrefs.SetInt(_key, value);
        }

        public int Get()
        {
            if (_value >= 0) return _value;
            _value =  PlayerPrefs.GetInt(_key, 0);
            return _value;
        }
    }
}