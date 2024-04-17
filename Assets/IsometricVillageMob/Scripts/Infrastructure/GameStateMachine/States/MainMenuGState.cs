using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Infrastructure.Containers;
using IsometricVillageMob.UI.Forms;

namespace IsometricVillageMob.Infrastructure.States
{
    public class MainMenuGState : IGState
    {
        [Inject] private readonly UIContainer _uiContainer;
        public void Enter(object data = null)
        {
            _uiContainer.GetForm<MainForm>().Show();
        }

        public void Exit()
        {
            _uiContainer.GetForm<MainForm>().Hide();
        }
    }
}