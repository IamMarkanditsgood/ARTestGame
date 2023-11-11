using System;
using Audio;
using UnityEngine;
using UnityEngine.UI;
using AudioType = Audio.AudioType;

namespace UI.Screens.GameMenu
{
    public class NumberButtons : MonoBehaviour
    {
        [SerializeField] private Button[] _buttons;

        private int _currentNumber;
        private AudioManager _audioManager;

        public event Action<int> OnAnswer;

        private void Start()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                int index = i;
                _buttons[i].onClick.AddListener(() => ButtonClicked(index));
            }
        }

        public void Initialize(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        private void ButtonClicked(int index)
        {
            _audioManager.PlaySound(AudioType.Effect, _audioManager.Sounds.PressButton);
            _currentNumber = index + 1;
            OnAnswer?.Invoke(_currentNumber);
        }

        public void SetInteractable(bool isInteractable)
        {
            foreach (Button button in _buttons)
            {
                button.interactable = isInteractable;
            }
        }
    }
}