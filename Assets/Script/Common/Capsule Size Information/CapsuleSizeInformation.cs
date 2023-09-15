using System;
using UnityEngine;

namespace Common.Capsule_Size_Information
{
    [Serializable]
    public class CapsuleSizeInformation : ICapsuleSizeInformation
    {
        [SerializeField] private float _height = 2f;
        [SerializeField] private float _radius = 0.5f;

        public float Height => _height;
        public float Radius => _radius;
    }
}