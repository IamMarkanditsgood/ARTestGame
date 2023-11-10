using System;
using UnityEngine;

namespace Entities.Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private bool _isInteractable;

        public event Action<int> OnAnswer;
        
        public bool IsInteractable
        {
            set => _isInteractable = value;
        }

        private void Update()
        {
            InteractWithWorld();
        }

        private void InteractWithWorld()
        {
            if (!_isInteractable) return;
            
            int number = GetResult();
            if (number != 0)
            {
                _isInteractable = false;
                OnAnswer?.Invoke(number);
            }
        }
        
        private int GetResult()
        {
            for (int index = 1; index <= 9; index++)
            {
                if (Input.GetKeyDown(index.ToString()))
                {
                    return index;
                }
            }
            return 0;
        }
    }
}
