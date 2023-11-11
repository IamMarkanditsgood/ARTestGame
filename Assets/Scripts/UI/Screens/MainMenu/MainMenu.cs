using TMPro;
using UnityEngine;

namespace UI.Screens.MainMenu
{
    public class MainMenu : BaseScreen
    {
        [SerializeField] private TMP_Text _text;

        public TMP_Text Text => _text;
    }
}