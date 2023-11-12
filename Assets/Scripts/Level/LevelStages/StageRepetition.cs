using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Audio;
using Entities.Numbers;
using Services;
using UnityEngine;
using AudioType = Audio.AudioType;

namespace Level.LevelStages
{
    [Serializable]
    public class StageRepetition: BaseStage
    {
        [SerializeField] private NumberControl _numberControl;
        [SerializeField] private bool _inUse;
        [SerializeField] private List<int> _availableNumbers = new();

        private LevelDataConfig _levelDataConfig;
        
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public event Action OnNextStage;
        
        public void Initialize(LevelDataConfig levelDataConfig, AudioManager audioManager)
        {
            base.Initialize(audioManager);
            _levelDataConfig = levelDataConfig;
            InitAvailableNumbers(10);
        }
        
        public override void PlayStage()
        {
            CheckAvailableNumbers();
        }

        private async Task WaitForNextNumber(CancellationToken cancellationToken)
        {
            TaskCompletionSource<bool> completionSource = new();
            cancellationToken.Register(() => completionSource.TrySetCanceled());
        
            await Task.Delay(_levelDataConfig.TimeBetweenNumbers * Constants.SecondsByMillisecond, cancellationToken);
            
            _inUse = false;
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }
        
        private void InitAvailableNumbers(int maxNumber)
        {
            for (int i = 1; i <= maxNumber; i++)
            {
                _availableNumbers.Add(i);
            }
        }

        private void CheckAvailableNumbers()
        {
            if (!_inUse)
            {
                switch (_availableNumbers.Count)
                {
                    case >= 1:
                        ContinueStage();
                        break;
                    case 0:
                        FinishStage();
                        break;
                }
            }
        }

        private async void ContinueStage()
        {
            ShowNextNumber();
            _inUse = true;
            await WaitForNextNumber(_cancellationTokenSource.Token);
        }

        private void FinishStage()
        {
            _inUse = true;
            _numberControl.gameObject.SetActive(false);
            OnNextStage?.Invoke();
        }
        
        private void ShowNextNumber()
        {
            int randomNumber = GetRandomNumber();
            AudioClip numberVoiceSound = AudioManager.Sounds.GetNumberVoice(randomNumber);
            AudioClip effectSound = AudioManager.Sounds.Appearing;
            AudioManager.PlaySound(AudioType.Voice, numberVoiceSound);
            AudioManager.PlaySound(AudioType.Effect, effectSound);
            _numberControl.SetNumber(randomNumber);
        }

        private int GetRandomNumber()
        {
            int randomIndex = UnityEngine.Random.Range(0, _availableNumbers.Count);
            int uniqueRandomNumber = _availableNumbers[randomIndex];
            _availableNumbers.RemoveAt(randomIndex);
            
            return uniqueRandomNumber;
        }
    }
    
}