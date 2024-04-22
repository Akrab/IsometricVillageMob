using System;
using System.Collections;
using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Infrastructure;
using IsometricVillageMob.Infrastructure.States;
using IsometricVillageMob.RuntimeData;
using IsometricVillageMob.UI.CustomComponents;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IsometricVillageMob.UI.Forms
{
    public class MainForm : BaseForm
    {
        [Inject] private IGameStateMachine _gameStateMachine;
        
        [Inject] private RuntimeContainer _runtimeContainer;
        [SerializeField] private ToggleGroup _toggleGroup;
        [SerializeField] private Toggle[] _toggles;
        [SerializeField] private ButtonExt _startBtn;

        private void OnClickStart()
        {
            var index = Array.IndexOf(_toggles, _toggleGroup.GetFirstActiveToggle());
            _runtimeContainer.ResourceCountMode = (ResourceCountMode)index;
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