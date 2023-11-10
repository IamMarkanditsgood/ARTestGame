using Entities.Numbers.Data;

namespace Entities.Level
{
    public abstract class BaseStage
    {
        protected InteractableNumbersData InteractableNumbers;
        
        public abstract void PlayStage();
    }
}
