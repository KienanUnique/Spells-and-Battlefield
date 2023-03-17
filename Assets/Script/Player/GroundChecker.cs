using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded { private set; get; } = false;
    public Action Land;
    public Action Fall;
    [SerializeField] private float _checkSphereRadius;
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
                Land?.Invoke();
                IsGrounded = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsGroundCollider(other))
        {
            _groundColliders.Remove(other);
            if (_groundColliders.Count == 0 && IsGrounded)
            {
                Fall?.Invoke();
                IsGrounded = false;
            }
        }
    }

    private bool IsGroundCollider(Collider colliderToCheck)
    {
        return (_groundMask.value & (1 << colliderToCheck.gameObject.layer)) > 0;
    }
}
