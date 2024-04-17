using System.Collections;
using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Infrastructure;
using IsometricVillageMob.Infrastructure.States;
using IsometricVillageMob.UI.CustomComponents;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IsometricVillageMob.UI.Forms
{
    public class MainForm : BaseForm
    {
        [Inject] private IGameStateMachine _gameStateMachine;
        [SerializeField] private ToggleGroup _toggleGroup;
        [SerializeField] private Toggle[] _toggles;
        [SerializeField] private ButtonExt _startBtn;

        private void OnClickStart()
        {
            StartCoroutine(ToGame());
        }

        private IEnumerator ToGame()
        {
            yield return SceneManager.LoadSceneAsync(CONSTANTS.GAME_SCENE, LoadSceneMode.Additive);
            _gameStateMachine.EnterToState<GameGState>();
        }

        protected override void setup()
        {
            for (int i = 0; i < _toggles.Length; i++)
            {
                _toggles[i].isOn = i == 0;
                _toggles[i].group = _toggleGroup;
                _toggleGroup.RegisterToggle(_toggles[i]);
            }

            _startBtn.onClick.AddListener(OnClickStart);
        }
    }
}