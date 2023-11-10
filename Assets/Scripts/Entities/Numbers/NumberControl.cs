using System.Collections.Generic;
using System.Linq;
using Entities.Numbers.Data;
using UnityEngine;

namespace Entities.Numbers
{
    public class NumberControl : MonoBehaviour
    {
        [SerializeField] private NumbersConfig _numbersConfig;
        [SerializeField] private MeshFilter _mesh = new();
        [SerializeField] private List<GameObject> _digits = new();
        
        private List<int> _number = new();

        public void SetNumber(int index)
        {
            List<int> number = GetDigitsList(index);
            SetModel(number);
        }
        
        private List<int> GetDigitsList(int number)
        {
            List<int> digitsList = new List<int>();

            do
            {
                int digit = number % 10;
                digitsList.Add(digit);
                number /= 10;
            } while (number > 0);

            if (digitsList.Count == 0)
            {
                digitsList.Add(0);
            }

            digitsList.Reverse();

            return digitsList;
        }

        private void SetModel(List<int> number)
        {
            _number = number;
            SetFirstDigitModel();
            CheckNextDigitsPresence();

        }
        
        private void SetFirstDigitModel()
        {
            Mesh newMesh = GetMesh(_number[0]);
            Material newMaterial = GetMaterial(_number[0]);
            _mesh.mesh = newMesh;
            gameObject.GetComponent<Renderer>().material = newMaterial;
        }

        private void CheckNextDigitsPresence()
        {
            if (_number.Count > 1)
            {
                SetNextDigitModels();
            }
            else
            {
                OffNextDigits();
            }
        }

        private void OffNextDigits()
        {
            for (int i = 0; i < _digits.Count; i++)
            {
                _digits[i].SetActive(false);
            }
        }
        
        private void SetNextDigitModels()
        {
            for (int i = 1; i < _number.Count; i++)
            {
                GameObject digit = GetNewPresenceDigit(i);
                SetNewDigitPosition(digit, i);
                SetNewDigitView(digit, i);
            }
        }

        private void SetNewDigitPosition(GameObject digit, int increaseIndex)
        {
            Vector3 position = gameObject.transform.position;
            var localScale = gameObject.transform.localScale;
                
            position.x -= localScale.x * increaseIndex;
            digit.transform.position = position;
            position.x += localScale.x * increaseIndex;
            gameObject.transform.position = position/2;
        }
        
        private void SetNewDigitView(GameObject digit,int numberIndex)
        {
            digit.GetComponent<MeshFilter>().mesh = GetMesh(_number[numberIndex]);
            digit.GetComponent<Renderer>().material = GetMaterial(_number[numberIndex]);
            digit.SetActive(true);
        }
        
        private GameObject GetNewPresenceDigit(int digitIndex)
        {
            return _digits.Count == _number.Count-1 ? _digits[digitIndex - 1] : CreateNewDigitObject();
        }
        
        private GameObject CreateNewDigitObject()
        {
            GameObject emptyObject = new GameObject("EmptyObject");
            GameObject newDigit = Instantiate(emptyObject, gameObject.transform, true);
            _digits.Add(newDigit);
            
            newDigit.AddComponent<MeshFilter>();
            newDigit.AddComponent<MeshRenderer>();
            
            return newDigit;
        }
        
        private Mesh GetMesh(int digit)
        {
            foreach (var number in _numbersConfig.NumbersData.Where(number => number.ID == digit))
            {
                return number.Mesh;
            }
            Debug.LogError("You do not have this digit!");
            return null;
        }

        private Material GetMaterial(int digit)
        {
            foreach (var number in _numbersConfig.NumbersData.Where(number => number.ID == digit))
            {
                return number.Material;
            }
            Debug.LogError("You do not have this digit!");
            return null;
        }
    }
}
