using System;
using UnityEngine;
using UnityEngine.UI;
using AudioType = Audio.AudioType;

namespace UI.Screens.GameResult
{
    public class GameResult : BaseScreen
    {
        [SerializeField] private Button _restart;

        public event Action OnRestart;

        private void Start()
        {
            _restart.onClick.AddListener(RestartClicked);
        }

        private void RestartClicked()
        {
            OnRestart?.Invoke();
            AudioManager.PlaySound(AudioType.Effect, AudioManager.Sounds.PressButton);
        }
    }
}