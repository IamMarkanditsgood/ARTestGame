using System;
using Entities.UI.Panels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entities.UI
{
    public class ScreensController : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private GameMenu _gameMenu;
        [SerializeField] private GameResult _gameResult;

        private BaseView _activeScreen;
        private BaseView[] _scenes;

        public event Action OnPlay;

        private void Start()
        {
            _activeScreen = _mainMenu;
            SetLevelPanels();
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
        
        private void Subscribe()
        {
            _mainMenu.OnPlay += ShowGameInterface;
            _gameResult.OnRestart += RestartLevel;
        }
        
        private void Unsubscribe()
        {
            _mainMenu.OnPlay -= ShowGameInterface;
            _gameResult.OnRestart -= RestartLevel;
        }
        private void SetLevelPanels() => _scenes = new BaseView[] {_mainMenu, _gameMenu, _gameResult};

        private void ShowScreen(BaseView nextScreen)
        {
            if (_activeScreen != null)
            {
                _activeScreen.Hide();
            }
            _activeScreen = nextScreen;
            nextScreen.Show();
        }

        private void ShowGameInterface()
        {
            OnPlay?.Invoke();
            ShowScreen(_scenes[1]);
        }

        private void RestartLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
        
        public void ShowGameResult()
        {
            ShowScreen(_scenes[2]);
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