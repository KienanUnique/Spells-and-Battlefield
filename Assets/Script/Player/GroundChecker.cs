using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded { private set; get; } = false;
    public Action Land;
    public Action Fall;
    [SerializeField] private float _checkSphereRadius;
    [SerializeField] private LayerMask _groundMask;

    private void Update()
    {
        var lastGroundedStatus = IsGrounded;
        IsGrounded = Physics.OverlapSphere(transform.position, _checkSphereRadius, _groundMask).Length > 0;
        if (lastGroundedStatus == false && IsGrounded == true)
        {
            Land?.Invoke();
        }
        else if (lastGroundedStatus == true && IsGrounded == false)
        {
            Fall?.Invoke();
        }
    }
}
