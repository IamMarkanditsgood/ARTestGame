using Sounds;
using UnityEngine;

namespace Entities.Numbers.Data
{
    public class InteractableNumbersData : MonoBehaviour
    {
        [SerializeField] private GameObject[] _numbers;
        [SerializeField] private SoundsConfig _sounds;
        [SerializeField] private AudioSource _soundSource;

        public void ShowInteractableNumber(int numberIndex)
        {
            SoundsManager.RunSound(_soundSource, _sounds.Appearing);
            _numbers[numberIndex-1].SetActive(true);
        }
        public GameObject GetNumber(int number)
        {
            return _numbers[number - 1];
        }

        public int GetIndex(GameObject choosedObject)
        {
            int index = 0;
            for (int i = 0; i < _numbers.Length; i++)
            {
                if (choosedObject == _numbers[i].gameObject)
                {
                    index = i + 1;
                    return index;
                }
            }
            return index;
        }
    }
}
