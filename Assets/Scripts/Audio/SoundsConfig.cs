using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "Sounds", menuName = "ScriptableObjects/Sounds")]
    public class SoundsConfig : ScriptableObject
    {
        [Header("UI")]
        [SerializeField] private AudioClip _pressButton;
        [Header("Effects")]
        [SerializeField] private AudioClip _appearing;
        [Header("Voices")]
        [SerializeField] private AudioClip _greeting;
        [SerializeField] private AudioClip _testStage;
        [SerializeField] private AudioClip[] _numbers;

        public AudioClip PressButton => _pressButton;

        public AudioClip Appearing => _appearing;

        public AudioClip Greeting => _greeting;

        public AudioClip TestStage => _testStage;

        public AudioClip GetNumberVoice(int number)
        {
           return _numbers[number - 1];
        }
    }
}
