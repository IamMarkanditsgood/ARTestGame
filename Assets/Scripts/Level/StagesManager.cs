using System;
using System.Threading;
using System.Threading.Tasks;
using Audio;
using Level.LevelStages;
using Services;
using UnityEngine;
using AudioType = Audio.AudioType;

namespace Level
{
    [Serializable]
    public class StagesManager
    {
        [SerializeField] private int _currentStageIndex;
        [SerializeField] private bool _isGameStarted;
        [SerializeField] private LevelDataConfig _levelDataConfig;
        [SerializeField] private StageRepetition _stageRepetition;
        [SerializeField] private StageTest _stageTest;

        private AudioManager _audioManager;
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
            _audioManager = AudioManager.Instance;
            _stageRepetition = new StageRepetition(_levelDataConfig, _audioManager);
            _stageTest = new StageTest(_levelDataConfig, _audioManager);
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
            _audioManager.PlaySound(AudioType.Voice, _audioManager.Sounds.Greeting);
            int delayTime = _audioManager.GetTimeOfSound(_audioManager.Sounds.Greeting) + 1;
            await WaitForVoice(_cancellationTokenSource.Token, delayTime);
        }
    }
}