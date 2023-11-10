using Entities.Level;
using Services;
using TMPro;
using UnityEngine;

namespace Entities.UI.Panels
{
    public class GameMenu : BaseView
    {
        [SerializeField] private TMP_Text _score;
        [SerializeField] private GameObject _wrongMessagePanel;
        [SerializeField] private GameObject _correctMessagePanel;
        [SerializeField] private LevelDataConfig _levelDataConfig;

        public void ShowNewScore(int score)
        {
            _score.SetText( score + "/10");
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
            var delayTime = _levelDataConfig.DelayTimeForOffPanel;
        
            yield return new WaitForSeconds(delayTime);

            panel.SetActive(false);
        }
    }
}
