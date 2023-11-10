using UnityEngine;

namespace Entities.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
    public class LevelDataConfig : ScriptableObject
    {
        [SerializeField] private int _timeBetweenNumbers = 1;
        [SerializeField] private int _timeBetweenStages = 5;
        [SerializeField] private int _scoreForWin = 10;
        [SerializeField] private float _delayTimeForOffPanel = 4;

        public int TimeBetweenNumbers => _timeBetweenNumbers;

        public int TimeBetweenStages => _timeBetweenStages;

        public int ScoreForWin => _scoreForWin;

        public float DelayTimeForOffPanel => _delayTimeForOffPanel;
    }
}