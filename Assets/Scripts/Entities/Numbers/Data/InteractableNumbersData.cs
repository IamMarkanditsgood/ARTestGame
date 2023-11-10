using UnityEngine;

namespace Entities.Numbers.Data
{
    public class InteractableNumbersData : MonoBehaviour
    {
        [SerializeField] private GameObject[] _numbers;

        public void ShowInteractableNumber(int numberIndex)
        {
            _numbers[numberIndex-1].SetActive(true);
        }
        public GameObject GetNumber(int number)
        {
            return _numbers[number - 1];
        }

        public int GetIndex(GameObject choosedObject)
        {
            for (int i = 0; i < _numbers.Length; i++)
            {
                if (choosedObject == _numbers[i].gameObject)
                {
                    return i + 1;
                }
            }
            return 0;
        }
    }
}
