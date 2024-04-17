using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Infrastructure;
using IsometricVillageMob.Infrastructure.Containers;
using IsometricVillageMob.Infrastructure.States;
using IsometricVillageMob.UI;
using UnityEngine;

namespace IsometricVillageMob.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [Inject] private UIContainer _uiContainer;
        [Inject] private IGameStateMachine _gameStateMachine;
        [SerializeField] private CanvasRoot _canvasRoot;

        public override void InstallBindings()
        {
            _diContainer.Inject(_canvasRoot);
            var forms = _canvasRoot.InitAndGetForms();
            foreach (var form in forms)
                _uiContainer.AddForm(form);
            
            _gameStateMachine.EnterToState<MainMenuGState>();
            
        }
    }
}