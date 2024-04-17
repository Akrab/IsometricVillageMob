using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Infrastructure.Containers;
using IsometricVillageMob.UI.Forms;

namespace IsometricVillageMob.Infrastructure.States
{
    public class GameGState : IGState
    {
        [Inject] private readonly UIContainer _uiContainer;
        public void Enter(object data = null)
        {
            _uiContainer.GetForm<CurrencyForm>().Show();
        }

        public void Exit()
        {
  
        }
    }
}