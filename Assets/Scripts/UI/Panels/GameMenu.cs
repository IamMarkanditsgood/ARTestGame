using TMPro;
using UnityEngine;

namespace UI.Panels
{
    public class GameMenu : BaseView
    {
        [SerializeField] private TMP_Text _score;
        [SerializeField] private GameObject _wrongMessagePanel;
        [SerializeField] private GameObject _correctMessagePanel;
        [SerializeField] private int _delayTimeForOffPanel;

        public void ShowNewScore(int value)
        {
            _score.SetText( $"Score: {value} /10");
        }

        public void ShowWrongAnswerMessage()
        {
            _wrongMessagePanel.SetActive(true);
            _correctMessagePanel.SetActive(false);
            StartCoroutine(HidePanelAfterDelay(_wrongMessagePanel));
        }
        
        public void ShowCorrectAnswersMessage()
        {
            _correctMessagePanel.SetActive(true);
            _wrongMessagePanel.SetActive(false);
            StartCoroutine(HidePanelAfterDelay(_correctMessagePanel));
        }
        
        private System.Collections.IEnumerator HidePanelAfterDelay(GameObject panel)
        {
            var delayTime = _delayTimeForOffPanel;
        
            yield return new WaitForSeconds(delayTime);

            panel.SetActive(false);
        }
    }
}
