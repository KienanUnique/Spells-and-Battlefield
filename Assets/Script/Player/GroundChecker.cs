using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded {private set; get;} = false;
    [SerializeField] private float _checkSphereRadius;
    [SerializeField] private LayerMask _groundMask;

    private void Update() {
        IsGrounded = Physics.OverlapSphere(transform.position, _checkSphereRadius, _groundMask).Length > 0;
    }
}
