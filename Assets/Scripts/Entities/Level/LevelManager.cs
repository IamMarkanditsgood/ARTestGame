using Entities.Level.LevelStages;
using Entities.Numbers.Data;
using Entities.UI;
using UnityEngine;

namespace Entities.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private ScreensController _screensController;
        [SerializeField] private StageRepetition _stageRepetition;
        [SerializeField] private StageTest _stageTest;
        [SerializeField] private InteractableNumbersData _interactableNumbers;
        [SerializeField] private int _currentStageIndex;
        [SerializeField] private bool _isGameStarted;
        
        private BaseStage _currentStage;
        private BaseStage[] _stagesInLevel;
        private int _score;

        private void Awake()
        {
            Subscribe();
            InitializeStages();
        }
        
        private void Update()
        {
            PlayStage();
        }

        private void OnDestroy()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            _screensController.OnPlay += StartGame;
            _stageRepetition.OnNextStage += ChangeCurrentStage;
            _stageTest.OnWin += WinGame;
            _stageTest.OnCorrectAnswer += CorrectAnswer;
            _stageTest.OnWrongAnswer += WrongAnswer;
        }
        
        private void UnSubscribe()
        {
            _screensController.OnPlay -= StartGame;
            _stageRepetition.OnNextStage -= ChangeCurrentStage;
            _stageTest.OnWin -= WinGame;
            _stageTest.OnCorrectAnswer -= CorrectAnswer;
            _stageTest.OnWrongAnswer -= WrongAnswer;
        }

        private void InitializeStages()
        {
            _stageRepetition.Initialize(_interactableNumbers);
            _stageTest.Initialize(_interactableNumbers, _score);
            _currentStage = _stageRepetition;
            _stagesInLevel = new BaseStage[] { _stageRepetition, _stageTest };
        }

        private void PlayStage()
        {
            if (_isGameStarted)
            {
                _currentStage.PlayStage();
            }
        }
        
        private void ChangeCurrentStage()
        {
            _currentStageIndex++;
            _currentStage = _stagesInLevel[_currentStageIndex];
        }

        private void StartGame()
        {
            _isGameStarted = true;
        }

        private void WinGame()
        {
            _isGameStarted = false;
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