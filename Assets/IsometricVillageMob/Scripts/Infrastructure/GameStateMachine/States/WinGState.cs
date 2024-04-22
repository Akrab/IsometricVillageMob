using Infrastructure.SaveLoad.Player;
using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Infrastructure.Containers;
using IsometricVillageMob.Infrastructure.Controllers.Timers;
using IsometricVillageMob.UI.Forms;

namespace IsometricVillageMob.Infrastructure.States
{
    public class WinGState : IGState
    {
        [Inject] private readonly TimerController _timerController;
        [Inject] private readonly UIContainer _uiContainer;
        [Inject] private readonly IPlayerInventory _playerInventory;
        
        public void Enter(object data = null)
        {
            _timerController.PauseTimers();
            if (_uiContainer.GetForm<MarketForm>().IsShow)
                _uiContainer.GetForm<MarketForm>().Hide();

            _uiContainer.GetForm<WinForm>().Show();

           
        }

        public void Exit()
        {
            _playerInventory.Clear();
            _uiContainer.GetForm<WinForm>().Hide();
            _uiContainer.GetForm<CurrencyForm>().Hide();
        }


    }
}