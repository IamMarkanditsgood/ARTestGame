using System.Collections.Generic;
using UnityEngine;

namespace Entities.Numbers.Data
{
    [CreateAssetMenu(fileName = "Numbers", menuName = "ScriptableObjects/NumbersCollection")]
    public class NumbersConfig : ScriptableObject
    {
        [SerializeField] private List<NumbersData> _numbersData;

        public List<NumbersData> NumbersData => _numbersData;
    }
}