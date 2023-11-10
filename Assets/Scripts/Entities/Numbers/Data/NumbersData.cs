using System;
using UnityEngine;

namespace Entities.Numbers.Data
{
    [Serializable]
    public class NumbersData
    {
        [SerializeField] private int _id;
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Material _material;

        public int ID => _id;
        public Mesh Mesh => _mesh;
        public Material Material => _material;
    }
}