using System;
using System.Threading;
using System.Threading.Tasks;
using Entities.Camera;
using Entities.Numbers.Data;
using UnityEngine;
using Random = System.Random;

namespace Entities.Level.LevelStages
{
    [Serializable]
    public class StageTest: BaseStage, IDisposable
    {
        [SerializeField] private WordsData[] _wordData;
        [SerializeField] private CameraManager _cameraManager;
        [SerializeField] private GameObject _questionWord;
        [SerializeField] private int _questionNumber;

        private Random _random = new();
        private int _score;
        private bool _waitForAnswer;
        private bool _isStageStarted;
        
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public event Action OnWin;
        public event Action OnCorrectAnswer;
        public event Action OnWrongAnswer;
        
        public  void Initialize(InteractableNumbersData interactableNumbers, int score)
        {
            InteractableNumbers = interactableNumbers;
            _score = score;
            Subscribe();
        }
        
        private void Subscribe()
        {
            _cameraManager.OnAnswer += CheckAnswer;
        }
        
        private void UnSubscribe()
        {
            _cameraManager.OnAnswer -= CheckAnswer;
        }
        
        public override void PlayStage()
        {
            CheckStageStatus();
        }

        public async Task WaitForNextStage(CancellationToken cancellationToken)
        {
            TaskCompletionSource<bool> completionSource = new();
            cancellationToken.Register(() => completionSource.TrySetCanceled());
            
            Debug.Log("Put voice for explenation");
            
            await Task.Delay(Constants.TimeBetweenStages * Constants.SecondsByMillisecond, cancellationToken);
            
            _isStageStarted = true;
        }
        
        public void Dispose()
        {
            UnSubscribe();
        }

        private async void CheckStageStatus()
        {
            if (!_isStageStarted)
            {
                await WaitForNextStage(_cancellationTokenSource.Token);
                return;
            }
            
            if (_score < Constants.ScoreForWin && !_waitForAnswer)
            {
                SetQuestion();
            }
            else if(_score == Constants.ScoreForWin)
            {
                OnWin?.Invoke();
            }
        }
        
        private void CheckAnswer(int answer)
        {
            if (answer == _questionNumber)
            {
                _score++;
                _waitForAnswer = false;
                _questionWord.SetActive(false);
                OnCorrectAnswer?.Invoke();
            }
            else
            {
                _cameraManager.IsInteractable = true;
                OnWrongAnswer?.Invoke();
            }
        }
        
        private void SetQuestion()
        {
            SetNumberForQuestion();
            SetWordForQuestion();
            
            _waitForAnswer = true;
            _cameraManager.IsInteractable = true;
        }

        private void SetNumberForQuestion()
        {
            int randomNumber = _random.Next(1, 11);
            _questionNumber = randomNumber;
        }
        
        private void SetWordForQuestion()
        {
            _questionWord = GetWord(_questionNumber);
            _questionWord.SetActive(true);
        }
        
        private GameObject GetWord(int number)
        {
            foreach (var word in _wordData)
            {
                if (word.ID == number)
                {
                    return word.Prefab;
                }
            }
            
            Debug.LogError("You do not have this word!");
            return null;
        }
    }
}