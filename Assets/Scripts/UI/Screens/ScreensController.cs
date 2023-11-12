using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Screens
{
    public class ScreensController : MonoBehaviour
    {
        [SerializeField] private MainMenu.MainMenu _mainMenu;
        [SerializeField] private GameMenu.GameMenu _gameMenu;
        [SerializeField] private GameResult.GameResult _gameResult;
        
        private BaseScreen _activeScreen;

        private void Start()
        {
            _activeScreen = _mainMenu;
        }
        
        private void ShowScreen(BaseScreen nextScreen)
        {
            if (_activeScreen != null)
            {
                _activeScreen.Hide();
            }
            _activeScreen = nextScreen;
            nextScreen.Show();
        }

        public void ShowGameMenu()
        {
            ShowScreen(_gameMenu);
        }

        public void ShowNumberButtons()
        {
            _gameMenu.ShowNumberButton();
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