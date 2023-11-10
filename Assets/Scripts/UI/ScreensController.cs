using System;
using Sounds;
using UI.Panels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class ScreensController : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private GameMenu _gameMenu;
        [SerializeField] private GameResult _gameResult;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private SoundsConfig _sounds;
        
        private BaseView _activeScreen;

        public event Action OnPlay;

        private void Start()
        {
            _activeScreen = _mainMenu;
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
        
        private void Subscribe()
        {
            _mainMenu.OnPlay += ShowGameMenu;
            _gameResult.OnRestart += RestartLevel;
        }
        
        private void Unsubscribe()
        {
            _mainMenu.OnPlay -= ShowGameMenu;
            _gameResult.OnRestart -= RestartLevel;
        }

        private void ShowScreen(BaseView nextScreen)
        {
            if (_activeScreen != null)
            {
                _activeScreen.Hide();
            }
            _activeScreen = nextScreen;
            nextScreen.Show();
        }

        private void ShowGameMenu()
        {
            SoundsManager.RunSound(_audioSource, _sounds.PressButton);
            OnPlay?.Invoke();
            ShowScreen(_gameMenu);
        }

        private void RestartLevel()
        {
            SoundsManager.RunSound(_audioSource, _sounds.PressButton);
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
        
        public void ShowGameResult()
        {
            ShowScreen(_gameResult);
        }

        public void ChangeScore(int newScore)
        {
            _gameMenu.ShowNewScore(newScore);
        }
        
        public void ShowWrongAnswerMessage()
        {
            _gameMenu.ShowWrongAnswerMessage();
        }
        
        public void ShowCorrectAnswersMessage()
        {
            _gameMenu.ShowCorrectAnswersMessage();
        }
    }
}