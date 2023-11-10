using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
    public class LevelDataConfig : ScriptableObject
    {
        [SerializeField] private int _timeBetweenNumbers = 2;
        [SerializeField] private int _scoreForWin = 10;

        public int TimeBetweenNumbers => _timeBetweenNumbers;
        
        public int ScoreForWin => _scoreForWin;
    }
}