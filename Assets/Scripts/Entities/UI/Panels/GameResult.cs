using System;
using UnityEngine;
using UnityEngine.UI;

namespace Entities.UI.Panels
{
    public class GameResult : BaseView
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
        }
    }
}
