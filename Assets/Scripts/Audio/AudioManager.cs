using System;
using Services;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public class AudioManager
    {
        [SerializeField] private SoundsConfig _sounds;
        [SerializeField] private AudioController _audioController;
        
        public SoundsConfig Sounds => _sounds;
        public static AudioManager Instance;

        private AudioManager()
        {
            Instance ??= this;
        }
        public void PlaySound(AudioType audioSourceType, AudioClip sound)
        {
            foreach (TypeValueDataService<AudioType, AudioSource> audioSource in _audioController.AudioSources)
            {
                if (audioSource.Type == audioSourceType)
                {
                    audioSource.Value.clip = sound;
                    audioSource.Value.Play();
                    return;
                }
            }
        }

        public int GetTimeOfSound(AudioClip sound)
        {
            int timeOfSound = (int)sound.length;
            return timeOfSound;
        }
    }
}