using Audio;

namespace Level
{
    public abstract class BaseStage
    {
        protected AudioManager AudioManager;

        public void Initialize(AudioManager audioManager)
        {
            AudioManager = audioManager;
        }
        public abstract void PlayStage();
        
    }
}