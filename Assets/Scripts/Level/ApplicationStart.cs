using Audio;
using Entities.Camera;
using UI;
using UI.Screens;
using UnityEngine;

namespace Level
{
    public class ApplicationStart : MonoBehaviour
    {
        [SerializeField] private ScreensController _screensController;
        [SerializeField] private CameraManager _cameraManager;
        [SerializeField] private StagesManager _stagesManager;
        [SerializeField] private AudioManager _audioManager;
        
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
            _cameraManager.OnPlay += StartGame;
            _stagesManager.StageRepetition.OnNextStage += _stagesManager.ChangeCurrentStage;
            _stagesManager.StageTest.OnWin += WinGame;
            _stagesManager.StageTest.OnCorrectAnswer += CorrectAnswer;
            _stagesManager.StageTest.OnWrongAnswer += WrongAnswer;
            _stagesManager.StageTest.OnStageStarted += _screensController.ShowNumberButtons;
        }
        
        private void Unsubscribe()
        {
            _cameraManager.OnPlay -= StartGame;
            _stagesManager.StageRepetition.OnNextStage -= _stagesManager.ChangeCurrentStage;
            _stagesManager.StageTest.OnWin -= WinGame;
            _stagesManager.StageTest.OnCorrectAnswer -= CorrectAnswer;
            _stagesManager.StageTest.OnWrongAnswer -= WrongAnswer;
            _stagesManager.StageTest.OnStageStarted -= _screensController.ShowNumberButtons;
        }

        private void StartGame()
        {
            _screensController.ShowGameMenu();
            _stagesManager.StartGame();
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