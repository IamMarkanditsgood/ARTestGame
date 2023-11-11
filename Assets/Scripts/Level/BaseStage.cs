using Audio;

namespace Level
{
    public abstract class BaseStage
    {
        protected readonly AudioManager AudioManager;

        public BaseStage(AudioManager audioManager)
        {
            AudioManager = audioManager;
        }
        public abstract void PlayStage();
        
    }
}