using System;
using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AudioType = Audio.AudioType;

namespace UI.Screens.GameMenu
{
    public class NumberButtons : MonoBehaviour
    {
        [SerializeField] private Button[] _buttons;
        [SerializeField] private bool _isInteractable;
        public TMP_Text text;
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
            text.SetText(_currentNumber.ToString());
            if (_isInteractable)
            {
                _audioManager.PlaySound(AudioType.UI, _audioManager.Sounds.PressButton);
                _currentNumber = index+1;
                OnAnswer?.Invoke(_currentNumber);
            }
            _currentNumber = 0;
        }

        public void SetInteractable(bool isInteractable)
        {
            _isInteractable = isInteractable;
        }
    }
}