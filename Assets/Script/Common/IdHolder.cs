using System;
using Interfaces;
using UnityEngine;

namespace Common
{
    public class IdHolder : MonoBehaviour, IIdHolder
    {
        public int Id { get; private set; }

        public int CompareTo(object obj)
        {
            if (obj is IIdHolder interactableObject)
            {
                return interactableObject.Id == Id ? 0 : 1;
            }

            throw new InvalidCastException();
        }

        private void Awake()
        {
            Id = gameObject.GetInstanceID();
        }
    }
}