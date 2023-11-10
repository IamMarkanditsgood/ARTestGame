using Entities.Numbers.Data;

namespace Level
{
    public abstract class BaseStage
    {
        protected InteractableNumbersData InteractableNumbers;

        public abstract void PlayStage();
    }
}
