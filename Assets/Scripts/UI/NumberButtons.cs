using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NumberButtons : MonoBehaviour
    {
        [SerializeField] private Button[] _buttons;
        [SerializeField] private bool _isInteractable;
        [SerializeField] private int _currentNumber;

        public event Action<int> OnAnswer;
        
        private void Start()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                int index = i;
                _buttons[i].onClick.AddListener(() =>ButtonClicked(index));
            }
        }

        private void ButtonClicked(int index)
        {
            if (_isInteractable)
            {
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