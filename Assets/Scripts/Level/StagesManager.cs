using System;
using System.Threading;
using System.Threading.Tasks;
using Entities.Numbers.Data;
using Level.LevelStages;
using Services;
using Sounds;
using UnityEngine;

namespace Level
{
    [Serializable]
    public class StagesManager
    {
        [SerializeField] private InteractableNumbersData _interactableNumbers;
        [SerializeField] private int _currentStageIndex;
        [SerializeField] private bool _isGameStarted;
        [SerializeField] private LevelDataConfig _levelDataConfig;
        [SerializeField] private StageRepetition _stageRepetition;
        [SerializeField] private StageTest _stageTest;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private SoundsConfig _sounds;
        
        private BaseStage _currentStage;
        private BaseStage[] _stagesInLevel;
        
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public StageRepetition StageRepetition
        {
            get => _stageRepetition;
            set => _stageRepetition = value;
        }
        public StageTest StageTest
        {
            get => _stageTest;
            set => _stageTest = value;
        }

        public bool IsGameStarted
        {
            get => _isGameStarted;
            set => _isGameStarted = value;
        }
        
        public void InitializeStages()
        {
            _stageRepetition.Initialize(_interactableNumbers, _levelDataConfig, _audioSource, _sounds);
            _stageTest.Initialize(_interactableNumbers, _levelDataConfig, _audioSource, _sounds);
            _currentStage = _stageRepetition;
            _stagesInLevel = new BaseStage[] { _stageRepetition, _stageTest };
        }

        public void PlayStage()
        {
            if (_isGameStarted)
            {
                _currentStage.PlayStage();
            }
        }
        public async Task WaitForVoice(CancellationToken cancellationToken, int delayTime)
        {
            TaskCompletionSource<bool> completionSource = new();
            cancellationToken.Register(() => completionSource.TrySetCanceled());

            await Task.Delay(delayTime * Constants.SecondsByMillisecond, cancellationToken);
            
            _isGameStarted = true;
        }
        
        public void ChangeCurrentStage()
        {
            _currentStageIndex++;
            _currentStage = _stagesInLevel[_currentStageIndex];
        }

        public async void StartGame()
        {
            SoundsManager.RunSound(_audioSource, _sounds.Greeting);
            int delayTime = SoundsManager.GetTimeOfSound(_sounds.Greeting) + 1;
            await WaitForVoice(_cancellationTokenSource.Token, delayTime);
        }
    }
}