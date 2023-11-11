using Services;
using UnityEngine;
using AudioType = Audio.AudioType;

namespace Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private TypeValueDataService<AudioType, AudioSource>[] _audioSources;

        public TypeValueDataService<AudioType, AudioSource>[] AudioSources => _audioSources;
    }
}