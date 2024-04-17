using IsometricVillageMob.DIIsometric;
using UnityEngine.SceneManagement;

namespace IsometricVillageMob.Infrastructure.States
{
    public class LoadingGState : IGState
    {
        [Inject] private IGameStateMachine _gameStateMachine;
        public void Enter(object data = null)
        {        
            SceneManager.LoadScene(CONSTANTS.UI_SCENE, LoadSceneMode.Additive);

        }

        public void Exit()
        {

        }
    }
}