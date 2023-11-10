using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Entities.Numbers;
using Entities.Numbers.Data;
using UnityEngine;

namespace Entities.Level.LevelStages
{
    [Serializable]
    public class StageRepetition: BaseStage
    {
        [SerializeField] private NumberControl _numberControl;
        [SerializeField] private bool _inUse;
        
        [SerializeField] private List<int> _availableNumbers = new();
        private System.Random _random = new();
        
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public event Action OnNextStage;
        
        public void Initialize(InteractableNumbersData interactableNumbers)
        {
            InteractableNumbers = interactableNumbers;
            InitAvailableNumbers();
        }
        
        public override void PlayStage()
        {
            CheckAvailableNumbers();
        }

        private async Task WaitForNextNumber(CancellationToken cancellationToken)
        {
            TaskCompletionSource<bool> completionSource = new();
            cancellationToken.Register(() => completionSource.TrySetCanceled());
        
            await Task.Delay(Constants.TimeBetweenNumbers * Constants.SecondsByMillisecond, cancellationToken);
            
            _inUse = false;
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }
        
        private void InitAvailableNumbers()
        {
            for (int i = 1; i <= 10; i++)
            {
                _availableNumbers.Add(i);
            }
        }

        private void CheckAvailableNumbers()
        {
            if (_availableNumbers.Count >= 1 && !_inUse)
            {
                ContinueStage();
            }
            else if(_availableNumbers.Count == 0 && !_inUse)
            {
                FinishStage();
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
            InteractableNumbers.ShowInteractableNumber(randomNumber);
            _numberControl.SetNumber(randomNumber);
        }

        private int GetRandomNumber()
        {
            int randomIndex = _random.Next(0, _availableNumbers.Count);
            int uniqueRandomNumber = _availableNumbers[randomIndex];
            _availableNumbers.RemoveAt(randomIndex);
            
            return uniqueRandomNumber;
        }
    }
}