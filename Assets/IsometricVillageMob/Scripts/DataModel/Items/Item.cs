using System;
using IsometricVillageMob.Game;
using UnityEngine;

namespace IsometricVillageMob.DataModel
{
    [CreateAssetMenu(fileName = "ItemsModel", menuName = "ScriptableObject/New Items")]
    public class Item : BaseModel
    {
        [SerializeField] private ItemData[] _data;

        public ItemData[] Data => _data;
    }

    [Serializable]
    public class ItemData
    {
        [SerializeField] private string _name;
        [SerializeField] private ItemType _itemType;
        [SerializeField] private Sprite _icon;

        [SerializeField] private int _cost;

        public string Name => _name;
        public ItemType ItemType => _itemType;
        public Sprite Icon => _icon;
        public int Cost => _cost;
    }
}
