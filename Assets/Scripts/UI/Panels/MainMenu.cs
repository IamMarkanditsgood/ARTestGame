using TMPro;
using UnityEngine;

namespace UI.Panels
{
    public class MainMenu : BaseView
    {
        [SerializeField] private TMP_Text _text;

        public TMP_Text Text => _text;
    }
}