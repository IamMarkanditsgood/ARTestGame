using UnityEngine;

namespace UI
{
    public abstract class BaseView: MonoBehaviour
    {
        [SerializeField] protected GameObject _screen;

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