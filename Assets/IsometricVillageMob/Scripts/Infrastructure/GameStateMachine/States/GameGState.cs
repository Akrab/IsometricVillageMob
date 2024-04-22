using Infrastructure.SaveLoad.Player;
using IsometricVillageMob.DataModel;
using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Game;
using IsometricVillageMob.Infrastructure.Containers;
using IsometricVillageMob.Infrastructure.Controllers.Timers;
using IsometricVillageMob.UI.Forms;
using UnityEngine;

namespace IsometricVillageMob.Infrastructure.States
{
    public class GameGState : IGState, ICurrencyListener
    {
        [Inject] private readonly TimerController _timerController;
        
        [Inject] private readonly UIContainer _uiContainer;
        [Inject] private readonly IGameStateMachine _gameStateMachine;
        [Inject] private readonly IPlayerInventory _playerInventory;
        [Inject] private readonly GameConfigModel _gameConfigModel;

        public void Enter(object data = null)
        {
            _timerController.RunTimers();
            _uiContainer.GetForm<CurrencyForm>().Show();
            _playerInventory.AddListener(this);
        }

        public void Exit()
        {
            _playerInventory.SubListener(this);
        }

        public void UpdateCurrency(CurrencyType currencyType, int newValue)
        {
            if (currencyType != CurrencyType.Gold) return;
            if (newValue >= _gameConfigModel.CurrencyForWin)
                _gameStateMachine.EnterToState<WinGState>();
        }
    }
}