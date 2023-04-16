using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GroundChecker : MonoBehaviour
{
    public event Action<bool> GroundStateChanged;
    public bool IsGrounded => _isGroundedWithReaction.Value;
    private ValueWithReactionOnChange<bool> _isGroundedWithReaction;
    [SerializeField] private LayerMask _groundMask;
    private List<Collider> _groundColliders;

    private void Awake()
    {
        _groundColliders = new List<Collider>();
        _isGroundedWithReaction = new ValueWithReactionOnChange<bool>(false);
    }

    private void OnEnable()
    {
        _isGroundedWithReaction.AfterValueChanged += OnAfterGroundedStateChanged;
    }

    private void OnDisable()
    {
        _isGroundedWithReaction.AfterValueChanged -= OnAfterGroundedStateChanged;
    }

    private void OnAfterGroundedStateChanged(bool newIsGrounded)
    {
        GroundStateChanged?.Invoke(newIsGrounded);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsGroundCollider(other))
        {
            _groundColliders.Add(other);
            if (!IsGrounded)
            {
                _isGroundedWithReaction.Value = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsGroundCollider(other)) return;

        _groundColliders.Remove(other);
        if (_groundColliders.Count == 0 && IsGrounded)
        {
            _isGroundedWithReaction.Value = false;
        }
    }

    private bool IsGroundCollider(Collider colliderToCheck)
    {
        return (_groundMask.value & (1 << colliderToCheck.gameObject.layer)) > 0;
    }
}