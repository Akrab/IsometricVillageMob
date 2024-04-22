using System.Collections;
using DG.Tweening;
using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Infrastructure;
using IsometricVillageMob.Infrastructure.States;
using IsometricVillageMob.UI.CustomComponents;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IsometricVillageMob.UI.Forms
{
    public class WinForm  : BaseForm
    {
        [Inject] private IGameStateMachine _gameStateMachine;
        [SerializeField] private ButtonExt _btnToMainMenu;
        
        private void ToMainMenu()
        {
            _btnToMainMenu.interactable = false;
            StartCoroutine(Exit());
        }

        private IEnumerator Exit()
        {
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(CONSTANTS.GAME_SCENE));
            _gameStateMachine.EnterToState<MainMenuGState>();
        }
        
        protected override void setup()
        {
            _btnToMainMenu.onClick.AddListener(ToMainMenu);
        }

        public override Tween Show(bool instance = false)
        {
            _btnToMainMenu.interactable = true;
            return base.Show(instance);
        }
    }
}
