using Sounds;
using UI;
using UnityEngine;

namespace Level
{
    public class StartApplication : MonoBehaviour
    {
        [SerializeField] private ScreensController _screensController;
        [SerializeField] private StagesManager _stagesManager = new();
        
        private int _score;

        private void Awake()
        {
            Subscribe();
            _stagesManager.InitializeStages();
        }
        
        private void Update()
        {
            _stagesManager.PlayStage();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _screensController.OnPlay += _stagesManager.StartGame;
            _stagesManager.StageRepetition.OnNextStage += _stagesManager.ChangeCurrentStage;
            _stagesManager.StageTest.OnWin += WinGame;
            _stagesManager.StageTest.OnCorrectAnswer += CorrectAnswer;
            _stagesManager.StageTest.OnWrongAnswer += WrongAnswer;
        }
        
        private void Unsubscribe()
        {
            _screensController.OnPlay -= _stagesManager.StartGame;
            _stagesManager.StageRepetition.OnNextStage -= _stagesManager.ChangeCurrentStage;
            _stagesManager.StageTest.OnWin -= WinGame;
            _stagesManager.StageTest.OnCorrectAnswer -= CorrectAnswer;
            _stagesManager.StageTest.OnWrongAnswer -= WrongAnswer;
        }
        
        private void WinGame()
        {
            _stagesManager.IsGameStarted = false;
            _screensController.ShowGameResult();
        }

        private void WrongAnswer()
        {
            _screensController.ShowWrongAnswerMessage();
        }

        private void CorrectAnswer()
        {
            _score++;
            _screensController.ChangeScore(_score);
            _screensController.ShowCorrectAnswersMessage();
        }
    }
}