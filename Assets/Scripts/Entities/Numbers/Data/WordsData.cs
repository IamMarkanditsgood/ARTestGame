using System;
using UnityEngine;

namespace Entities.Numbers.Data
{
    
    public class WordsData: MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private GameObject _prefab;
        
        public int ID => _id;
        public GameObject Prefab => _prefab;
    }
}