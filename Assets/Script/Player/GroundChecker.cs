using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(BoxCollider))]
    public class GroundChecker : MonoBehaviour
    {
        public bool IsGrounded { private set; get; }
        [SerializeField] private LayerMask _groundMask;
        private List<Collider> _groundColliders;

        private void Awake()
        {
            _groundColliders = new List<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsGroundCollider(other))
            {
                _groundColliders.Add(other);
                if (!IsGrounded)
                {
                    IsGrounded = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!IsGroundCollider(other)) return;

            _groundColliders.Remove(other);
            if (_groundColliders.Count == 0 && IsGrounded)
            {
                IsGrounded = false;
            }
        }

        private bool IsGroundCollider(Collider colliderToCheck)
        {
            return (_groundMask.value & (1 << colliderToCheck.gameObject.layer)) > 0;
        }
    }
}