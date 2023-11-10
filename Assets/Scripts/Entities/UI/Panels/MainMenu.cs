using System;
using UnityEngine;
using UnityEngine.UI;

namespace Entities.UI.Panels
{
    public class MainMenu : BaseView
    {
        [SerializeField] private Button _play;

        public event Action OnPlay;
        
        private void Start()
        {
            _play.onClick.AddListener(PlayClicked);
        }

        private void PlayClicked()
        {
            OnPlay?.Invoke();
        }
    }
}