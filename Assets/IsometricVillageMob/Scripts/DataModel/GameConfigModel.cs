using UnityEngine;

namespace IsometricVillageMob.DataModel
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObject/New GameConfig" )]
    public class GameConfigModel : ScriptableObject
    {
        [SerializeField] private int _currencyForWin;

        public int CurrencyForWin => _currencyForWin;
    }
}