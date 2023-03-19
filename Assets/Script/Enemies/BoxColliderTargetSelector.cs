using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(BoxCollider))]
    public class BoxColliderTargetSelector : MonoBehaviour
    {
        private List<ICharacter> _charactersInside;
        public List<ICharacter> GetTargetsInCollider() => _charactersInside;

        private void Awake()
        {
            _charactersInside = new List<ICharacter>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICharacter character))
            {
                _charactersInside.Add(character);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ICharacter character) && _charactersInside.Contains(character))
            {
                _charactersInside.Remove(character);
            }
        }
    }
}