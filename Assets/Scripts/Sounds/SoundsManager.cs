using UnityEngine;

namespace Sounds
{
    public static class SoundsManager
    {
        public static void RunSound(AudioSource audioSource,AudioClip sound)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }

        public static int GetTimeOfSound(AudioClip sound)
        {
            int timeOfSound = (int)sound.length;
            return timeOfSound;
        }
    }
}