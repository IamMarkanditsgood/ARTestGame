using Audio;
using UnityEngine;

namespace UI.Screens
{
    public abstract class BaseScreen: MonoBehaviour
    {
        [SerializeField] protected GameObject _screen;
        protected readonly AudioManager AudioManager = AudioManager.Instance;
        
        public virtual void Show()
        {
            _screen.SetActive(true);
        }

        public virtual void Hide()
        {
            _screen.SetActive(false);
        }
    }
}