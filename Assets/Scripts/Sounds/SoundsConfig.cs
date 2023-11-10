using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu(fileName = "Sounds", menuName = "ScriptableObjects/Sounds")]
    public class SoundsConfig : ScriptableObject
    {
        [SerializeField] private AudioClip _pressButton;
        [SerializeField] private AudioClip _interactions;
        [SerializeField] private AudioClip _appearing;
        [SerializeField] private AudioClip _greeting;
        [SerializeField] private AudioClip _testStage;
        [SerializeField] private AudioClip[] _numbers;

        public AudioClip PressButton => _pressButton;

        public AudioClip Interactions => _interactions;
        
        public AudioClip Appearing => _appearing;

        public AudioClip Greeting => _greeting;

        public AudioClip TestStage => _testStage;

        public AudioClip GetNumberVoice(int number)
        {
           return _numbers[number - 1];
        }
    }
}
